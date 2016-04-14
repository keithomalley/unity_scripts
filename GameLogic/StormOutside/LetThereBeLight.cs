using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class LetThereBeLight : MonoBehaviour {
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
	public GameObject light_object;
	private GameObject gameController;
	private Transform light_switch;	
	public bool switch_activated;
	private int rotation_degrees = 15;
	public bool light_on = false;
    private AudioSource source;

	// run when the gameobject is loaded
	void Start () {
        m_InteractiveItem = GetComponent<VRInteractiveItem>();
		light_switch = this.gameObject.transform.FindChild("Switch");
		switch_activated = false;
		source = GetComponent<AudioSource>();
		gameController = GameObject.FindWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		// when the switch is activated  run the function to handle this
		if (switch_activated) {
			ToggleLightSwitch();
			switch_activated = false;
		}
	}
	
    
    
    // function to toggle the switch's state
	void ToggleLightSwitch () {
        
        // toggles the rotation of the switch between on and off
		if(light_on){
        	light_switch.transform.localEulerAngles = new Vector3(rotation_degrees, 0, 0);
		} else {
        	light_switch.transform.localEulerAngles = new Vector3(-1 * rotation_degrees, 0, 0);
		}
		
        // play the switch flicked sound effect
		source.Play();
		
		if( gameController.GetComponent<GameController>().power == true ){
			// toggle the light on/off
			ToggleLight();
		}
		
		light_on = !light_on;
			
	}
    
    
    // function to toggle the lights enabled status
    public void ToggleLight(){
        light_object.SetActive(!light_object.activeInHierarchy);
    }
    
    
    
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
    
	void Interact(){
        // toggles a boolean for use in the update function
        switch_activated = true;
	}
    
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
