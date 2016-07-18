using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CampaignStageManager : MonoBehaviour {

	LobbyManager lobbyManager; 
	GameObject activeStage;

	public delegate void OnChangeActiveStageHandler(string stageName);
	public event OnChangeActiveStageHandler OnChangeActiveStage;

	public GameObject[] stageBoardList;
	public int currentStage = 0;

	// Use this for initialization
	void Start () {
			SortStageBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		GamePadControl ();
	}
	void OnEnable() {
		
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;

		lobbyManager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		lobbyManager.OnTapObject += OnTapObject;

	}

	void OnTapObject (string stageName)
	{
		
	}

	void OnDisable() {
		OVRTouchpad.TouchHandler -= GearTouchHandler;
		lobbyManager.OnTapObject -= OnTapObject;
	}

	void GearTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.Left:
			currentStage++;
			SortStageBoard ();
			break;
		case OVRTouchpad.TouchEvent.Right:
			currentStage--;
			SortStageBoard ();
			break;
		}

	}

	void GamePadControl(){
		if (Input.GetAxis ("Oculus_GearVR_DpadX") < 0f) {
			currentStage--;
			SortStageBoard ();
			System.Threading.Thread.Sleep (200);
		} else if (Input.GetAxis ("Oculus_GearVR_DpadX") > 0f) {
			currentStage++;
			SortStageBoard ();
			System.Threading.Thread.Sleep (200);
		}

	}

	void SortStageBoard() {

		if (currentStage > (stageBoardList.Length - 1)) {
			currentStage = stageBoardList.Length - 1;
			return;
		} else if (currentStage < 0) {
			currentStage = 0;
			return;
		}

		for ( int i = 0; i < stageBoardList.Length; i++) {
			if (i == currentStage) {
				stageBoardList [i].GetComponent<Renderer> ().sortingLayerName = "ActiveStage";
				stageBoardList [i].transform.DOScale (new Vector3 (0.9f, 0.7f, 1.0f), 0.5f);
				stageBoardList [i].transform.DOMoveZ (0.3f,0.5f);
				OnChangeActiveStage (stageBoardList [i].name);
				continue;
			}

			stageBoardList [i].GetComponent<Renderer> ().sortingLayerName = "OtherStage";
			stageBoardList [i].transform.DOScale (new Vector3 (0.5f, 0.4f, 1.0f), 0.5f);
			stageBoardList [i].transform.DOMoveZ (1.0f, 0.5f);

		}

	}

}
