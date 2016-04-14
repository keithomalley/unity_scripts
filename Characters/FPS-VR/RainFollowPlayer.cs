using UnityEngine;
using System.Collections;

public class RainFollowPlayer : MonoBehaviour {
    
    /*============================================================================
    
    This script positions the rain particle effect 20 units above the player at 
    all times, allowing for a smaller particle effect to be used and conserving 
    processing power with little noticeable effect on the players experience
    
    ============================================================================*/
    
    private Transform player;

	// Use this for initialization
	void Start () {
	   player = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	   this.transform.position = new Vector3(player.position.x, player.position.y + 20, player.position.z);
	}
}
