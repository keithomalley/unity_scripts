using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class Pickup : MonoBehaviour {
    
    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    public string type = "";
    private PlayerInventory player_inv;

	// Use this for initialization
	void Start () {
	   player_inv = GameObject.FindWithTag("MainCamera").GetComponent<PlayerInventory>();
	}
    
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
    
    void Interact(){
        if (type != ""){
            Debug.Log("Picked up " + type);
            if(type == "key"){
                player_inv.AddItem(type, gameObject);
            } else {
                player_inv.AddItem(type);
            }
            
            gameObject.SetActive(false);            
        }
	}
    
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
