using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour {
    
    /*============================================================================
    
    This script keeps track of the players inventory
    
    ============================================================================*/
	
    public int has_fuse_count = 0;
    public bool has_fuse_two;
    public bool has_flashlight;
    private GameObject flashlight;
	private AudioSource flashlight_source;
    public List<GameObject> keys = new List<GameObject>();
    
	void Start () {
        has_flashlight = false;
        flashlight = GameObject.Find("Flashlight");
        flashlight_source = GetComponent<AudioSource>();
        flashlight.SetActive(has_flashlight);
	}
	
	void Update () {
	   if( Input.GetButtonDown("Flashlight") && has_flashlight ){
			flashlight.SetActive( !flashlight.activeInHierarchy );
			flashlight_source.Play();
		}
	}
    
    public void AddItem(string type){
        switch (type){
            case "flashlight" :
                has_flashlight = true;
                flashlight.SetActive( !flashlight.activeInHierarchy );
			    flashlight_source.Play();
                break;
            case "fuse" :
                has_fuse_count++;
                break;
            case "key" :
                Debug.Log("Key pickup");
                break;
            default:
                Debug.Log("default pickup");
                break;
        }
    }
    
    public void AddItem(string type, GameObject target){
        switch (type){
            case "key" :
                keys.Add(target);
                Debug.Log("Key pickup");
                break;
            default:
                Debug.Log("default pickup");
                break;
        }
    }
    
    public bool HasKey(GameObject key){
        if(keys.Count > 0 && keys.Contains(key)){
            return true;
        } else {
            return false;
        }
    }
}
