using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    
	/*============================================================================
    
    This script Controls the main Game Logic for the whole game.
    
    It contains a reference to each of the lightswitches in the house, 
    as well as every thing that needs to know whether the power is working 
    or not, like the alarm and fusebox. 
    
    It can use these references to turn on and off the power, and to control 
    the state of the game.
    
    It also knows what stage the game is at, and can set up various objects 
    when notified.
    
    
    ============================================================================*/
    
	public bool power;
    public bool alarm_power = true;
    //private bool oculus_connected;
    private string power_state;
    
    public GameObject roof;
    
    public GameObject radio;
    public GameObject radio_model;
    public GameObject tv;
    public List<GameObject> lights;
    public GameObject broken_radio;
    
    public GameObject glass;
    public GameObject smashedglass;
    public GameObject kitchen_trigger;
    public GameObject bloodstains;
    public GameObject last_door;
    public GameObject second_last_door;
    
    // Alarm code logic
    public GameObject alarm_display_text;
    public string numcode = "2102";
    public string numcode_input = "";
    public string alarm_text = "Enter code";
    
    // game state management
    public int gamestate = 1;
    
    private AudioSource audioSource;
    public AudioClip lightflickerClip;
    public AudioClip doorslamClip;
    
    
    
    // These functions run after a set period of time using Coroutines
    IEnumerator FlickerLightsAfter(float time){
        yield return new WaitForSeconds(time);
        FlickerLights();
    }
    
    IEnumerator SetAjarAfter(float time){
        yield return new WaitForSeconds(time);
        second_last_door.GetComponent<DoorAnimation>().SetAjar();
    }
    
    IEnumerator LockAfterSlamming(float time){
        yield return new WaitForSeconds(time);
        last_door.GetComponent<DoorAnimation>().enabled = true;
        last_door.GetComponent<DoorAnimation>().locked = true;
        last_door.GetComponent<DoorAnimation>().open = false;
        last_door.GetComponent<DoorAnimation>().ajar = false;
    }
    
    IEnumerator EndTheGameAfter(float time){
        yield return new WaitForSeconds(time);
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    
    
    
    
    
    void Start(){
        roof.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }
    
	
	public void togglePower(){
		power = !power;
        if(power == true){ 
            power_state = "On"; 
            // turn on everything
            radio.GetComponent<RadioInteraction>().ToggleRadioSwitch();
            
            // turn on each of the lights that have their in the "on" position
            for (int i = 0; i < lights.Count; i++){
                if( lights[i].GetComponent<LetThereBeLight>().light_on){
                    lights[i].GetComponent<LetThereBeLight>().ToggleLight();
                }
            }
            // set the alarm text to be allowed to input code
            alarm_display_text.GetComponent<Text>().text = alarm_text;
        } else { 
            // keep track of power state
            power_state = "Off"; 
        }
        // Debug info
        Debug.Log("Power is: " + power_state );
        
	}
    
    public void SmashGlass(){
        glass.SetActive(false);
        smashedglass.SetActive(true);
        bloodstains.SetActive(true);
    }
    
    public void AlarmButtonPush(int button){
        if(alarm_power && power){
            //Debug.Log(button);
            numcode_input += ""+button+"";
            alarm_display_text.GetComponent<Text>().text = numcode_input;
            alarm_display_text.GetComponent<Text>().enabled = true;
            if(numcode_input.Length == 4){
                if(numcode_input == numcode){
                    alarm_power = false;
                    alarm_display_text.GetComponent<Text>().enabled = true;
                    alarm_display_text.GetComponent<Text>().text = "Power Restored";
                    SmashGlass();
                    radio_model.SetActive(false);
                    kitchen_trigger.SetActive(true);
                    broken_radio.SetActive(true);
                } else {
                    numcode_input = "";
                    alarm_display_text.GetComponent<Text>().text = "Enter Code";
                }
            }
        }
    }
    
    public void BeginningOfTheEnd(){
        Debug.Log("The Gamestate is: " + gamestate);
        
        switch(gamestate){
            case 1:
                last_door.GetComponent<DoorAnimation>().SetAjar();
                break;
            case 2:
                last_door.GetComponent<Animator>().SetBool("Slammed", true);
                last_door.GetComponent<DoorAnimation>().enabled = false;
                StartCoroutine( LockAfterSlamming(2.5f) );
                audioSource.clip = doorslamClip;
                audioSource.Play();
                StartCoroutine( SetAjarAfter(1.5f) );
                break;
            case 3:
                audioSource.clip = lightflickerClip;
                audioSource.Play();
                StartCoroutine( FlickerLightsAfter(1.5f) );
                break;
            case 4:
                StartCoroutine( EndTheGameAfter(3.0f) );
                break;
        }
        gamestate++;
    }
    
    public void FlickerLights(){
        for (int i = 0; i < lights.Count; i++){
            if( lights[i].GetComponent<LetThereBeLight>().light_on == true){
                lights[i].GetComponent<LetThereBeLight>().ToggleLight();
            }
        }
        togglePower();
    }
    
    
	/*
	void Update(){
		// Check inputs from PS3 Controller
		
		if ( Input.GetButtonDown("Start") ) {
			Debug.Log( "Start was pressed" );
		} else if ( Input.GetButtonDown("Select") ) {
			Debug.Log( "Select was pressed" );
		} else if ( Input.GetButtonDown("L1") ) {
			Debug.Log( "L1 was pressed" );
		} else if ( Input.GetButtonDown("L2") ) {
			Debug.Log( "L2 was pressed" );
		} else if ( Input.GetButtonDown("L3") ) {
			Debug.Log( "L3 was pressed" );
		} else if ( Input.GetButtonDown("R1") ) {
			Debug.Log( "R1 was pressed" );
		} else if ( Input.GetButtonDown("R2") ) {
			Debug.Log( "R2 was pressed" );
		} else if ( Input.GetButtonDown("R3") ) {
			Debug.Log( "R3 was pressed" );
		} else if ( Input.GetButtonDown("Circle") ) {
			Debug.Log( "Circle was pressed" );
		} else if ( Input.GetButtonDown("Square") ) {
			Debug.Log( "Square was pressed" );
		} else if ( Input.GetButtonDown("Triangle") ) {
			Debug.Log( "Triangle was pressed" );
		} else if ( Input.GetButtonDown("Cross") ) {
			Debug.Log( "Cross was pressed" );
		} else if ( Input.GetButtonDown("Dpad_Up") ) {
			Debug.Log( "Dpad_Up was pressed" );
		} else if ( Input.GetButtonDown("Dpad_Down") ) {
			Debug.Log( "Dpad_Down was pressed" );
		} else if ( Input.GetButtonDown("Dpad_Left") ) {
			Debug.Log( "Dpad_Left was pressed" );
		} else if ( Input.GetButtonDown("Dpad_Right") ) {
			Debug.Log( "Dpad_Right was pressed" );
		}
	}*/

}
