using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class Drawer_Interaction : MonoBehaviour {
    
    /*============================================================================
    
    This script governs the logic for the chest of drawers in the master bedroom
    Each of the drawers can be interacted with as a VRInteractiveItem, and has a 
    component to reflect that.
    
    ============================================================================*/
    
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    
	private Vector3 open_position;
	private Vector3 closed_position;
	private float open_distance = 0.2f;
    
    // keep track of whether the drawer is open or closed
	public bool open = false;
	public bool activated = false;
	private float smoothing = 2.0f;

	// Use this for initialization
	void Start () {
		closed_position = transform.localPosition;
		open_position = new Vector3( closed_position.x, closed_position.y, closed_position.z + open_distance );
	}
	
	// Update is called once per frame
	void Update () {
		// when the switch is activated 
		if (activated) {
			open = !open;
			activated = false;
		}
		if ( open ) {
			transform.localPosition = Vector3.Slerp(transform.localPosition, open_position, Time.deltaTime * smoothing);
		} else {
			transform.localPosition = Vector3.Slerp(transform.localPosition, closed_position, Time.deltaTime * smoothing);
		}
	}
	
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
	
	void Interact(){
        // set the bool activated to true, allowing the update function to use it  
        // to control the coded animation from drawer states
        activated = true;
	}
    
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
