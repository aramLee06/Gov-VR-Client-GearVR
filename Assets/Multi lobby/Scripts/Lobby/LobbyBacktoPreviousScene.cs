using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LobbyBacktoPreviousScene : MonoBehaviour {

	public GameObject OVRCamera1;
	public GameObject optionBoard;
	public GameObject nameBoard;

	public static bool sc_check = false;

	void Start(){
	}

	void Update(){
		OVRCamera1 = GameObject.Find("OVRCameraRig");
		optionBoard = GameObject.Find ("Option");
		nameBoard = GameObject.Find ("PlayerName");

		if (sc_check == false) {
			this.transform.DOMoveY (1.5f, 1.0f);
		} else if(sc_check == true){
			this.transform.DOMoveY (6f, 1.0f);
			optionBoard.transform.DOMove (new Vector3(-1.5f, 0.7f, 0.13f), 1.0f);
			nameBoard.transform.DOMove (new Vector3(1.5f, 0.75f, 0.13f), 1.0f);
		}
	}

	public void BackToPrevious(){
		this.transform.DOMoveY (6f, 1.0f);
		OVRCamera1.transform.DOMoveZ (-1.2f, 1.0f);
		sc_check = true;
		Invoke ("PrevAnim", 1.0f);
	}

	void PrevAnim(){
		SceneManager.LoadScene ("02_Lobby_new");
	}
}
