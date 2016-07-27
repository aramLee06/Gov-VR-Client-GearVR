using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MLP_Manager : Photon.MonoBehaviour {

	public CloudRegionCode ServerHost = CloudRegionCode.us;

	public Image hostPanel;
	public Image clientPanel;
	public Image roomPanel;

	public byte playerIdx = 1;
	public string GameVersion = "1.0";

	private int idx = 0;
	private bool stageChk = false;
	private bool joining = false;

	void Awake () {
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.automaticallySyncScene = true;
		PhotonNetwork.autoJoinLobby = true;
		//PhotonNetwork.ConnectUsingSettings ("v1.0");

		if (!PhotonNetwork.connected)
		{
			Connect();
		}
	}

	void Start () {
		//Network - Join Lobby
		PhotonNetwork.JoinLobby ();

		//PlayerPrefs - RoomNumber Index
		PlayerPrefs.SetInt ("RoomNumber", idx);
	}

	void Update () {
		//Back Button
		if (Input.GetKey (KeyCode.Escape)) {
			if (stageChk == true) {
				roomPanel.gameObject.SetActive (false);
				hostPanel.gameObject.SetActive (true);
				clientPanel.gameObject.SetActive (true);
				stageChk = false;
			} else if (stageChk == false) {
				SceneManager.LoadScene ("02_Lobby_new");
			}
		}
	}

	void Connect(){
		
		if (string.IsNullOrEmpty(PhotonNetwork.PhotonServerSettings.AppID))
		{
			Debug.LogWarning("You need your appID, read the documentation for more info!");
			return;
		}
		//Connect to select region
		PhotonNetwork.ConnectToRegion(ServerHost, GameVersion);
	}

	public void CreateRoomAsHost(){

		RoomOptions options = new RoomOptions();
		options.maxPlayers = playerIdx;

		PhotonNetwork.CreateRoom(PlayerPrefs.GetInt("RoomNumber").ToString(), options, TypedLobby.Default);

		PlayerPrefs.SetInt("RoomNumber", idx++);

		roomPanel.gameObject.SetActive (true);
		hostPanel.gameObject.SetActive (false);
		clientPanel.gameObject.SetActive (false);
		stageChk = true;
	}

	/*
	public override void OnJoinedRoom(){

	}

	public override void OnCreatedRoom (){

	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer){

	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer disPlayer){

	}
	*/
}