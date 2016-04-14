using UnityEngine;
using System.Collections;

public class TurnOnTv : MonoBehaviour {
    
    /*============================================================================
    
    This script is attached to a physical box collider that when walked into by 
    the player, turns the tv on and sets the tvroom door to be ajar
    
    ============================================================================*/

	public GameObject tv;
    public GameObject tv_door;
    void OnTriggerEnter(Collider other){
        tv.GetComponent<TV_Static>().TogglePower();
        tv_door.GetComponent<DoorAnimation>().SetAjar();
    }
}
