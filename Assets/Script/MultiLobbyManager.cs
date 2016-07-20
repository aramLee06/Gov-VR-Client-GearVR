using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MultiLobbyManager : MonoBehaviour {

	MainLobbyManager lobbyManager;

	public GameObject UnitModel;
	public GameObject CreateRoom;
	public GameObject WaitRoom;
	public GameObject CreateRoomBoard;
	public GameObject WaitRoomBoard;
	public GameObject OVRCamera;

	public GameObject[] stageBoardList;
	public int currentStage = 0;
	public bool flag=true;

	void OnEnable() {
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;

		lobbyManager = GameObject.Find ("GameManager").GetComponent<MainLobbyManager> ();
		lobbyManager.OnTapObject += OnTapObject;

		CreateRoom.SetActive (false);
		WaitRoom.SetActive (false);

		CreateRoomBoard.SetActive (true);
		WaitRoomBoard.SetActive (true); 
	}

	void OnDisable() {
		OVRTouchpad.TouchHandler -= GearTouchHandler;
		lobbyManager.OnTapObject -= OnTapObject;
	}


	void OnTapObject (string boardName)
	{
		//GameObject.Find ("TestText").GetComponent<TextMesh> ().text = boardName;
		switch (boardName) {
		case "CreateRoomBoard": 
			//CreateRoomBoard.transform.DOLocalMoveX (-2.0f, 2.0f);
			//WaitRoomBoard.transform.DOLocalMoveX (2.0f, 2.0f);
			flag = false;
			StartCoroutine (SetNonActive ());
			CreateRoomBoard.SetActive (false);
			WaitRoomBoard.SetActive (false);
			CreateRoom.SetActive (true);
			UnitModel.SetActive (false);
			OVRCamera.transform.DOMove (new Vector3 (0, 1.0f, -0.6f), 1.0f);
			break;
		case "WaitRoomBoard":
			//CreateRoomBoard.transform.DOLocalMoveX (-2.0f, 2.0f);
			//WaitRoomBoard.transform.DOLocalMoveX (2.0f, 2.0f);
			flag = false;
			StartCoroutine (SetNonActive ());
			CreateRoomBoard.SetActive (false);
			WaitRoomBoard.SetActive (false);
			WaitRoom.SetActive (true);
			UnitModel.SetActive (false);
			OVRCamera.transform.DOMove (new Vector3 (0, 1.0f, -0.6f), 1.0f);
			break;
		}

	}

	void GearTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.Left:
			currentStage++;
			SortStateBoard ();
			break;
		case OVRTouchpad.TouchEvent.Right:
			currentStage--;
			SortStateBoard ();
			break;
		}
	}
		
	IEnumerator SetNonActive() {
		yield return new WaitForSeconds (2.0f);

		CreateRoomBoard.SetActive (false);
		WaitRoomBoard.SetActive (false);
	}

	void Start () {

		SortStateBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		SortStateBoard ();
		if (Input.GetKey (KeyCode.Escape)) {
			ToMainLobby ();
		}
	}

	void ToMainLobby() {
		//CreateRoomBoard.transform.localPosition = new Vector3 (-0.5f, 0.0f, 0.0f);
		//WaitRoomBoard.transform.localPosition = new Vector3 (0.5f, 0.0f, 0.0f);
		OVRCamera.transform.DOMove (new Vector3 (0, 0.85f, -1.2f), 1.0f);
		CreateRoomBoard.SetActive (true);
		WaitRoomBoard.SetActive (true);
		CreateRoom.SetActive (false);
		WaitRoom.SetActive (false);
	}


	void SortStateBoard() {

		if (currentStage > (stageBoardList.Length - 1)) {
			currentStage = stageBoardList.Length - 1;
			return;
		} else if (currentStage < 0) {
			currentStage = 0;
			return;
		}

		for ( int i = 0; i < stageBoardList.Length; i++) {
			if (i == currentStage) {
				stageBoardList [i].transform.DOScale (new Vector3 (1f, 1f, 1f), 0.5f);
				stageBoardList [i].transform.DOMoveZ (0.3f,0.5f);
				continue;
			}
			stageBoardList [i].transform.DOScale (new Vector3 (0.8f, 0.8f, 0.8f), 0.5f);
			stageBoardList [i].transform.DOMoveZ (0.6f, 0.5f);

		}

	}

	void SelectUnits(string unitName){

	}
}