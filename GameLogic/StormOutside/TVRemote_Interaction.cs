using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class TVRemote_Interaction : MonoBehaviour {
    
    /*============================================================================
    
    This script governs the logic for the tv remote control, toggling the power 
    for the tv when the power has been restored
    
    ============================================================================*/
    
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
	public bool activated;
    private GameController gc;
    public GameObject tv;
    
    void Start () {
        activated = false;
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}
	
	void Update () {
        if (activated == true && gc.power == true) {
            tv.GetComponent<TV_Static>().TogglePower();
            activated = false;
        }
	}
    
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
    
    void Interact () {
		activated = true;
	}
    
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
