using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class PushAlarmButton : MonoBehaviour {
    
    /*============================================================================
    
    This script governs the logic for the alarm's buttons, alerting the 
    gamecontroller when they have been interacted with, and playing a sound
    
    ============================================================================*/
    
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    public int numpad_btn = 0;
    private GameController gc;
    private AudioSource audioSource;
    
    void Start(){
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
    }
    
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
    
    void Interact(){
        if(gc.alarm_power){
            gc.AlarmButtonPush(numpad_btn);
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
