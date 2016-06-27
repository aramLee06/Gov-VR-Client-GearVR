using UnityEngine;
using System.Collections;

public class MultiLobbyManager : MonoBehaviour {

	public GameObject CreateRoomBoard;
	public GameObject CreateRoom;
	public GameObject WaitRoomBoard;
	public GameObject WaitRoom;

	//public delegate void OnChangeActiveStageHandler(string stageName);
	//public event OnChangeActiveStageHandler OnChangeActiveStage;

	void OnEnable() {
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;
	}

	void OnDisable() {
		OVRTouchpad.TouchHandler -= GearTouchHandler;
	}

	void GearTouchHandler (object sender, System.EventArgs e) {
		
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.Left:
			break;
		case OVRTouchpad.TouchEvent.Right:
			break;
		case OVRTouchpad.TouchEvent.SingleTap:
			LobbySelected ();
			break;
		}

	}

	// Use this for initialization
	void Start () {
	
		CreateRoom.SetActive (false);
		WaitRoom.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LobbySelected() {
		GameObject selectedBoard = GameObject.Find ("aim").GetComponent<TrackingManager> ().trackedItem;

		switch (selectedBoard.name) {
		case "CreateRoomBoard":
			CreateRoom.SetActive (true);
			WaitRoom.SetActive (false);


			break;
		case "WaitRoomBoard":
			CreateRoom.SetActive (false);
			WaitRoom.SetActive (true);
			break;
		}
	}
}







