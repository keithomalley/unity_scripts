using UnityEngine;
using System.Collections;

public class TriggerLightning : MonoBehaviour {
    
    /*============================================================================
    
    This script triggers a flash of lightning, by activating a flashing light 
    object used for lightning
    
    ============================================================================*/

    
    public GameObject lightningobject;
	
	void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player") {
            lightningobject.GetComponent<Flashing_Light>().lightComponent.enabled = true;
            lightningobject.GetComponent<Flashing_Light>().changeTime = Time.time + lightningobject.GetComponent<Flashing_Light>().timeOn;
            gameObject.SetActive(false);
        }
    }
}
