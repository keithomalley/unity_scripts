using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Flashing_Text : MonoBehaviour {
    
    /*============================================================================
    
    This script simply flashes a combination of a text component, and a light 
    component on and off, by taking the desired time periods as float values and 
    checking them against a third value. 
    
    The third value is set by adding each value to the value of the current time, 
    and checking whether this the current time is greater than this time.
    
    It also plays and stops the Audiosource attached to the gameObject by turns
    
    This is used to display the alarm code and flash its text, as well as signal 
    to the player with the light and audio beeping noise.
    
    ============================================================================*/
    
    public float timeOn = 0.1f;
    public float timeOff = 0.5f;
    private float changeTime = 0.0f;
    private Text textComponent;
    private Light lightComponent;
    private AudioSource audioSource;
    
	// Use this for initialization
	void Start () {
       lightComponent = GetComponent<Light>();
	   textComponent = transform.FindChild("Canvas").FindChild("Text").gameObject.GetComponent<Text>();
       audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > changeTime) {
            lightComponent.enabled = !lightComponent.enabled;
            textComponent.enabled = !textComponent.enabled;
            if (textComponent.enabled) {
                audioSource.Play();
                changeTime = Time.time + timeOn;
            } else {
                audioSource.Stop();
                changeTime = Time.time + timeOff;
            }
        }
	}
}
