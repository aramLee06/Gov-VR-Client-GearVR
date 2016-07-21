﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LobbyBacktoPreviousScene : MonoBehaviour {

	public GameObject OVRCamera1;

	public static bool sc_check = false;

	void Update(){
		OVRCamera1 = GameObject.Find("OVRCameraRig");
		if (sc_check == false) {
			this.transform.DOMoveY (1.2f, 1.0f);
		} else if(sc_check == true){
			this.transform.DOMoveY (5f, 1.0f);
		}
	}

	public void BackToPrevious(){
		this.transform.DOMoveY (5f, 1.0f);
		OVRCamera1.transform.DOMoveZ (-1.2f, 1.0f);
		sc_check = true;
		Invoke ("PrevAnim", 1.0f);
	}

	void PrevAnim(){
		SceneManager.LoadScene ("02_Lobby_new");
	}
}
