using UnityEngine;
using System.Collections;

public class RadioInteraction : MonoBehaviour {
    // A simple function to toggle the state of the radio's audio and light components
    public void ToggleRadioSwitch(){
        GetComponent<AudioSource>().Play();
        GetComponent<Light>().enabled = true;
    }
}
