using UnityEngine;
using System.Collections;

public class ActivateObject : MonoBehaviour {
    /*============================================================================
    
    This script takes a gameobject as a parameter and toggles the 
    gameobject as active when the player collides with whatever the script
    is attached to. 
    
    It then deactivates the gameobject that it is attached to, effectively acting
    as a trigger for events to happen in the game
    
    ============================================================================*/
    
    public GameObject g;
    
    // OnTriggerEnter is called when a gameobject collides with the physics collider 
    // on the object this script is attached to
	void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            g.SetActive(!g.activeInHierarchy);
            gameObject.SetActive(false);
        }
    }
}
