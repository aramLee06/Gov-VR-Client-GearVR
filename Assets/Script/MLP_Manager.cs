using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MLP_Manager : Photon.MonoBehaviour {
	
	public GameObject ovrCamera;

	public Image hostPanel;
	public Image clientPanel;
	public Image roomPanel;
	public Canvas mainMultiLobby;

	private int idx = 0;
	private bool stageChk = false;

	void Update () {
		//Back Button
		if (Input.GetKey (KeyCode.Escape)) {
			if (stageChk == true) {
				//roomPanel.gameObject.SetActive (false);
				mainMultiLobby.gameObject.SetActive(false);
				hostPanel.gameObject.SetActive (true);
				clientPanel.gameObject.SetActive (true);
				ovrCamera.transform.DOMoveZ (-0.72f, 1.0f);
				stageChk = false;
			} else if (stageChk == false) {
				SceneManager.LoadScene ("02_Lobby_new");

			}
		}
	}

	public void CreateRoomAsHost(){
		//roomPanel.gameObject.SetActive (true);
		mainMultiLobby.gameObject.SetActive(true);
		hostPanel.gameObject.SetActive (false);
		clientPanel.gameObject.SetActive (false);
		ovrCamera.transform.DOMoveZ (-0.2f, 1.0f);
		stageChk = true;
	}
}