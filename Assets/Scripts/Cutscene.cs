using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {
	
	public GameObject CutsceneCam; //Set the cutscene cam object
	public GameObject NormalCam; //Set the normal cam object
	public bool InCutscene=false; //Set if we are in cutscene
	public int Timer; //Timer adds 1 per frame
	public int Count; //What cutscene is played
	
	/*
	
	If you want to use cutscene system write
	"public Cutscene Cutscene;" at the start of your's class
	
	If you want to change the Cutscene that is played
	do "Cutscene.Count=X;" (X must be = int)
	
	If you want to change cutscene status do "Cutscene.Enable();" to enable
	and "Cutscene.Disable();" to disable
	
	If you want to change the cutscene cam do "Cutscene.CutsceneCam=X;" (X must be = GameObject)
	If you want to change the normal cam do "Cutscene.NormalCam=X;" (X must be = GameObject)
	
	Want to know what frame the cutscene is in?
	do "X=Cutscene.Timer;" (X must be = int)
	
	Want to know if you are in cutscene?
	do "X=Cutscene.InCutscene;" (X must be = bool)
	
	*/
	
	void Start () {
		//Chek if we start on cutscene
		if (InCutscene) {
			Enable(); 
		} else {
			Disable();
		}
	}
	
	void Update () {
		Timer++; //Add one to the timer
	}
	
	public void Enable () { //Enable the cutscene
		CutsceneCam.SetActive(true); //Enable Cutscene cam
		NormalCam.SetActive(false); //Disable Noraml cam
		InCutscene=true; //Set the in cutscene status to true
		Timer=0; //Reset the timer
	}
	public void Disable (){ //Disable the cutscene
		CutsceneCam.SetActive(false); //Disable Cutscene cam
		NormalCam.SetActive(true); //Enable Noraml cam
		InCutscene=false; //Set the in cutscene status to false
	}
}
