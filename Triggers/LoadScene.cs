using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    
    /*============================================================================
    
    This script simply loads the main scene by name when the player walks into the 
    attached gameobject
    
    ============================================================================*/
    
   public string scene_name;

	void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene(scene_name);
        }
    }
}
