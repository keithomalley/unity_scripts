using UnityEngine;
using System.Collections;

public class TheBeginningOfTheEnd : MonoBehaviour {
    
    /*============================================================================
    
    This script increments the GameController's state variable when the player 
    walks into the object this script is attached to.
    
    This is used to keep track of where in the games timeline the player is
    
    ============================================================================*/
    
    private GameController gc;
    
    void Start(){
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            // run the function on the gamecontroller to update the gamestate
            gc.BeginningOfTheEnd();
            gameObject.SetActive(false);
        }
    }

}
