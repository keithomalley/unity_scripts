using UnityEngine;
using System.Collections;

public class TV_Static : MonoBehaviour {
    
    /*============================================================================
    
    This script swaps out the material on the tv screen for a "static" shader 
    material, and plays the audio when the tv is on
    
    ============================================================================*/
    
    public Material staticMat;
    public Material normalMat;
    public AudioSource audioSource;
    public bool tv_on = false;

	// Use this for initialization
	void Start () {
	   audioSource = GetComponent<AudioSource>();
	}
	
	public void TogglePower(){
        tv_on = !tv_on;
        if (tv_on) {
           GetComponent<Renderer>().material = staticMat;
           audioSource.Play();
        } else {
           GetComponent<Renderer>().material = normalMat;
           audioSource.Stop();
        }
    }
}
