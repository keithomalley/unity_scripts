using UnityEngine;
using System.Collections;

public class AlarmLogic : MonoBehaviour {
    
    /*============================================================================
    
    This script governs the logic for the alarm. Setting the light and text to 
    stop flashing so long as the house's power is restored and the input code 
    has been entered correctly.
    
    It contains a variable for the GameController's script and another to it's 
    child object, the alarm display which has a flashing light and text
    
    ============================================================================*/
    
    private GameController gc;
    public GameObject display;

	// Function run when this script is first loaded
	void Start () {
        // find the GameController object by its tag and store a reference to its GameController script
	   gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if(gc.alarm_power == false && gc.power == true){
            display.GetComponent<AudioSource>().Stop();
            display.GetComponent<Flashing_Text>().enabled = false;
            display.GetComponent<Light>().enabled = false;
        }
	}
}
