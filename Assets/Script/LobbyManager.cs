using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class LobbyManager : MonoBehaviour {

	TrackingManager trackingManager;
	GamepadConnectionManager gp_conManager;

	public GameObject campaignBoard;
	public GameObject multiBoard;
	public GameObject unitSelect;
	public GameObject OVRCamera;

	public GameObject CampaignLobby;
	public GameObject MultiLobby;

	public delegate void OnTapObjectHandler(string stageName);

	public event OnTapObjectHandler OnTapObject;

	//public GameObject UnitSelectLobby;

	private string beforeBoard;

	// Use this for initialization
	void Start () {
		gp_conManager = GameObject.Find ("GameManager").GetComponent<GamepadConnectionManager> ();
		trackingManager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		CampaignLobby.SetActive (false);
		MultiLobby.SetActive (false);
	}

	void OnEnable() {
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;
	}

	void OnDisable() {
		OVRTouchpad.TouchHandler -= GearTouchHandler;
	}

	void GearTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.SingleTap:
			CheckSelectedMode (trackingManager.trackedItem.name);
			break;
		}
	}
		

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			ToMainLobby ();
		}

	}

	//Gamepad connecting Manager
	void FixedUpdate(){
		if (Input.GetAxis ("Oculus_GearVR_RIndexTrigger") != 0f) {

			//Select Board
			CheckSelectedMode (gp_conManager.curr_Select.name);
			System.Threading.Thread.Sleep (200);

		} else if (Input.GetAxis ("Oculus_GearVR_LIndexTrigger") != 0f) {

			//Back to Main
			ToMainLobby ();
			System.Threading.Thread.Sleep (200);

		}
	}

	void CheckSelectedMode(string itemName) {

		switch (itemName) {
		case "Campaign_Board": 
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);

			StartCoroutine(SetNonActive());
			CampaignLobby.SetActive (true);
			break;
		case "Multi_Board":
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);

			StartCoroutine(SetNonActive());
			MultiLobby.SetActive (true);
			break;
		case "UnitSelect":
			OVRCamera.transform.DOMove (new Vector3 (0, 0.45f, 0.2f), 2.0f);

			break;

		default :
			OnTapObject (itemName);
			break;
		}
		//Check The Board Level
		gp_conManager.m_BoardLevel++;

	}

	void ToMainLobby() {
		multiBoard.SetActive (true);
		campaignBoard.SetActive (true);
		multiBoard.transform.DOMove (new Vector3 (0.4f, 0.85f, 0.8f), 2.0f);
		campaignBoard.transform.DOMove (new Vector3 (-0.4f, 0.85f, 0.8f), 2.0f);
		OVRCamera.transform.DOMove (new Vector3 (0, 0.85f, -0.73f), 2.0f);
		CampaignLobby.SetActive (false);
		MultiLobby.SetActive (false);

		//Check the Board Level
		gp_conManager.m_BoardLevel=0;
	}


	IEnumerator SetNonActive() {
		yield return new WaitForSeconds (2.0f);

		multiBoard.SetActive (false);
		campaignBoard.SetActive (false);
	}

}
