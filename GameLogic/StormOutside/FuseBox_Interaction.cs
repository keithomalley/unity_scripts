using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class FuseBox_Interaction : MonoBehaviour {
    
	[SerializeField] private VRInteractiveItem m_InteractiveItem;
	public bool activated;
	public GameObject player;
	public GameObject gameController;
    public GameObject[] fuses;
    public GameObject[] fuse_lights;
    public int fuses_inserted = 1;

	// Use this for initialization
	void Start () {
		activated = false;
		player = GameObject.FindWithTag("MainCamera");
		gameController = GameObject.FindWithTag("GameController");
	}
	
	void Update () {
		// when the switch is activated 
		if (activated) {
            // check whether the player has the correct amount of fuses
			if( player.GetComponent<PlayerInventory>().has_fuse_count > 0 && fuses_inserted < 4 ){
                
                // if they do, remove one and set the corresponding fuse child of the box to be enabled
                player.GetComponent<PlayerInventory>().has_fuse_count--;
                fuses[fuses_inserted].SetActive(true);
                fuse_lights[fuses_inserted].GetComponent<Light>().enabled = true;
                
                // increment the amount of inserted fuses
                fuses_inserted++;
                
                // if enough fuses have been placed in the box, turn on the power
                if(fuses_inserted == 4 && gameController.GetComponent<GameController>().power == false) {
                    gameController.GetComponent<GameController>().togglePower();
                }
			}
			activated = false;
		}
	}
    
    
    
    /*============================================================================
        The Unity VR Assets work by triggering the following Interact function when
        a certain button is pressed and the first person reticle overlaps with the
        object that this script is attached to.
    ============================================================================*/
	
	void Interact () {
        // toggles a boolean value used in the update function to check for interactions
		activated = true;
	}
	
    private void OnEnable(){
        m_InteractiveItem.OnDown += Interact;
    }
	
    private void OnDisable(){
        m_InteractiveItem.OnDown -= Interact;
    }
}
