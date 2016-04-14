using UnityEngine;
using System.Collections;

public class Flashing_Light : MonoBehaviour {
    
    /*============================================================================
    
    This script simply flashes a light component on and off, by taking the desired 
    time periods as float values and checking them against a third value. 
    
    The third value is set by adding each value to the value of the current time, 
    and checking whether this the current time is greater than this time.
    
    ============================================================================*/
    
    public float timeOn = 0.1f;
    public float timeOff = 0.5f;
    public float changeTime = 0.0f;
    public Light lightComponent;
    
	// Use this for initialization
	void Start () {
	   lightComponent = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        // check if the time period has passed
        if (Time.time > changeTime) {
            //toggle the lights enabled status
            lightComponent.enabled = !lightComponent.enabled;
            
            // set the next time change point based on whether the light is on or off
            if (lightComponent.enabled) {
                changeTime = Time.time + timeOn;
            } else {
                changeTime = Time.time + timeOff;
            }
        }
	}
    
}
