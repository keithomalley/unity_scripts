using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class ViewOnPickup : MonoBehaviour {
    
    /*============================================================================
    
    This script governs allows the player to pick up and look at items such as notes, 
    moving them to an empty gameobject in front of the player and parenting them 
    to that so that the player can move around with them. When the player interacts 
    with them again they are returned to their original position and rotation
    
    ============================================================================*/

    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    
    // variables to track of the original position and rotation of the object to be viewed 
    private Vector3 startPosition;
    private Quaternion startRotation;
    
    // the players position to be moved to
    private Vector3 playerPosition;
    // smoothing of the Lerp animation
	private float smoothing = 7.0f;
    private bool picked_up = false;
    
    // the gameobjects to parent the item to
    private GameObject pickupsContainer;
    private GameObject itemViewer;
        
	
	void Start () {
        // get a reference to original transform orientation on start
	    startPosition = transform.position;
        startRotation = transform.rotation;
        
        // get a reference to the parent gameobjects on start
        pickupsContainer = GameObject.Find("Pickups");
        itemViewer = GameObject.Find("ItemViewer");
	}
    
    // code called every frame
    void Update() {
        if (picked_up){
            transform.SetParent(itemViewer.transform);
            playerPosition = itemViewer.transform.position;
            transform.position = Vector3.Slerp(transform.position, playerPosition, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( GameObject.Find("MainCamera").transform.position - transform.position ), Time.deltaTime * smoothing );
        } else {
            transform.SetParent(pickupsContainer.transform);
            transform.position = Vector3.Slerp(transform.position, startPosition, Time.deltaTime * smoothing);
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * smoothing);
        }
    }
    
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
	
    
    void Interact(){
        picked_up = !picked_up;
	}
    
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
