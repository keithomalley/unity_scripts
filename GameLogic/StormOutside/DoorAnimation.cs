using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class DoorAnimation : MonoBehaviour {
    
    /*============================================================================
    
    This script controls the animation states of a door object. Deciding whether 
    a door is open, closed or ajar, or even locked, and playing the appropriate sounds.
    
    ============================================================================*/
    
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    
    // references to each of the sound effects
    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip lockedClip;
    public AudioClip unlockedClip;
    private AudioSource audioSource;
    
    // booleans to track states
    public bool locked = false;
    public bool requireKey = false;
        
    public bool ajar;
    public bool open;
    
    // a key gameobject for unlocking the door, and the player object, whiose inventory will contain the key
    public GameObject key;
    private GameObject player;
        
    // the doors animator
    private Animator anim;

	void Awake () {
        // set up references
        m_InteractiveItem = gameObject.GetComponent<VRInteractiveItem>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
	}
    
	void Update () {
        anim.SetBool("Ajar", ajar);
        anim.SetBool("Open", open);
        if (anim.IsInTransition(0) && !audioSource.isPlaying){
            if(open){
                audioSource.clip = openClip;
            } else {
                audioSource.clip = closeClip;
            }
            audioSource.Play();
        }
	}
    
    // public function to set a door ajar
    public void SetAjar(){
        ajar = true;
        open = false;
        locked = false;
        requireKey = false;
    }
    
        
    void Interact(){
        if(!locked){
            ajar = false;
            open = !open;
        }
        
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
        
        if(locked && requireKey) {
            if(player.GetComponent<PlayerInventory>().HasKey(key) ){
                Debug.Log("unlocked with key");
                locked = false;
                audioSource.clip = unlockedClip;
            } else {
                Debug.Log("locked & requires key");
                audioSource.clip = lockedClip;
            }
           audioSource.Play();
        } else if(locked) {
            Debug.Log("locked");
            audioSource.clip = lockedClip;
            audioSource.Play();
        }
	}
    
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
