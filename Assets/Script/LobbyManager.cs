using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class LobbyManager : MonoBehaviour {

	GamepadConnectionManager gpdManager;
	TrackingManager trackingManager;

	public GameObject campaignBoard;
	public GameObject multiBoard;
	public GameObject unitSelect;

	public GameObject OVRCamera;
	public GameObject UnitSelectLobby;
	public GameObject CampaignLobby;
	public GameObject MultiLobby;
	public GameObject UnitsBoard;
	public GameObject WeaponsBoard;

	public delegate void OnTapObjectHandler(string stageName);
	public event OnTapObjectHandler OnTapObject;

	public bool stageChk = true;

	private string beforeBoard;

	// Use this for initialization
	void Start () {

		gpdManager = GameObject.Find ("GameManager").GetComponent<GamepadConnectionManager> ();
		trackingManager = GameObject.Find ("aim").GetComponent<TrackingManager> ();

		CampaignLobby.SetActive (false);
		MultiLobby.SetActive (false);
		UnitsBoard.SetActive (false);
		WeaponsBoard.SetActive (false);
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

	//Gamepad Left Right Trigger Control
	void FixedUpdate(){
		if (Input.GetAxis ("Oculus_GearVR_RIndexTrigger") != 0f) {

			//Select Board
			CheckSelectedMode (gpdManager.curr_Select.name);
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
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);

			unitSelect.transform.DOScale (new Vector3 (0.8f, 0.8f, 0.8f), 1.0f);
			OVRCamera.transform.DOMove (new Vector3 (0, 0.45f, 0.2f), 1.0f);

			UnitsBoard.SetActive (true);
			WeaponsBoard.SetActive (true);

			UnitsBoard.transform.DOMoveX (-0.6f, 1.0f);
			WeaponsBoard.transform.DOMoveX (0.6f, 1.0f);
			break;

		default :
			OnTapObject (itemName);
			break;
		}

		stageChk = false;
	}

	void ToMainLobby() {
		multiBoard.SetActive (true);
		campaignBoard.SetActive (true);

		multiBoard.transform.DOMove (new Vector3 (0.4f, 0.85f, 0.8f), 2.0f);
		campaignBoard.transform.DOMove (new Vector3 (-0.4f, 0.85f, 0.8f), 2.0f);
		OVRCamera.transform.DOMove (new Vector3 (0, 0.85f, -0.73f), 1.0f);

		UnitsBoard.transform.DOMoveX (-2.0f, 1.0f);
		WeaponsBoard.transform.DOMoveX (2.0f, 1.0f);

		CampaignLobby.SetActive (false);
		MultiLobby.SetActive (false);
		UnitsBoard.SetActive (false);
		WeaponsBoard.SetActive (false);

		if (gpdManager.curr_Select.name == "UnitSelect") {
			gpdManager.curr_Select.transform.DOScale (new Vector3(1.3f,1.3f,1.3f), 1.0f);
		}

		stageChk = true;
	}


	IEnumerator SetNonActive() {
		yield return new WaitForSeconds (2.0f);

		multiBoard.SetActive (false);
		campaignBoard.SetActive (false);
	}

}
