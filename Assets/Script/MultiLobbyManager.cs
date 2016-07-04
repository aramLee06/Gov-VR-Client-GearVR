using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MultiLobbyManager : MonoBehaviour {

	public GameObject CreateRoom;
	public GameObject WaitRoom;

	public GameObject CreateRoomBoard;
	public GameObject WaitRoomBoard;
	LobbyManager lobbyManager; 

	void OnEnable() {
		lobbyManager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		lobbyManager.OnTapObject += OnTapObject;

		CreateRoom.SetActive (false);
		WaitRoom.SetActive (false);

		CreateRoomBoard.SetActive (true);
		WaitRoomBoard.SetActive (true); 
	}


	void OnTapObject (string boardName)
	{
		//GameObject.Find ("TestText").GetComponent<TextMesh> ().text = boardName;
		switch (boardName) {
		case "CreateRoomBoard": 
			//CreateRoomBoard.transform.DOLocalMoveX (-2.0f, 2.0f);
			//WaitRoomBoard.transform.DOLocalMoveX (2.0f, 2.0f);

			//StartCoroutine(SetNonActive());
			CreateRoomBoard.SetActive (false);
			WaitRoomBoard.SetActive (false);
			CreateRoom.SetActive (true);
			break;
		case "WaitRoomBoard":
			//CreateRoomBoard.transform.DOLocalMoveX (-2.0f, 2.0f);
			//WaitRoomBoard.transform.DOLocalMoveX (2.0f, 2.0f);

			//StartCoroutine(SetNonActive());
			CreateRoomBoard.SetActive (false);
			WaitRoomBoard.SetActive (false);
			WaitRoom.SetActive (true);
			break;
		}

	}

	IEnumerator SetNonActive() {
		yield return new WaitForSeconds (2.0f);

		CreateRoomBoard.SetActive (false);
		WaitRoomBoard.SetActive (false);
	}

	void OnDisable() {
		lobbyManager.OnTapObject -= OnTapObject;
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			ToMainLobby ();
		}
	}

	void ToMainLobby() {
		//CreateRoomBoard.transform.localPosition = new Vector3 (-0.5f, 0.0f, 0.0f);
		//WaitRoomBoard.transform.localPosition = new Vector3 (0.5f, 0.0f, 0.0f);
		CreateRoomBoard.SetActive (true);
		WaitRoomBoard.SetActive (true);
		CreateRoom.SetActive (false);
		WaitRoom.SetActive (false);
	}


}







