using UnityEngine;
using UnityEngine.SceneManagement;
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

	Renderer c_Rend;
	Renderer m_Rend;

	// Use this for initialization
	void Start () {

		gpdManager = GameObject.Find ("GameManager").GetComponent<GamepadConnectionManager> ();
		trackingManager = GameObject.Find ("aim").GetComponent<TrackingManager> ();

		c_Rend = campaignBoard.GetComponent<Renderer> ();
		m_Rend = multiBoard.GetComponent<Renderer> ();

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

			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_on") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;

			StartCoroutine (SetNonActive ());
			CampaignLobby.SetActive (true);
			stageChk = false;
			break;

		case "Multi_Board":
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);

			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_on") as Texture;

			Invoke ("SceneMove", 1.0f);

			StartCoroutine (SetNonActive ());
			//MultiLobby.SetActive (true);
			stageChk = false;
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

		multiBoard.transform.DOMove (new Vector3 (0.6f, 0.7f, 0.6f), 1.0f);
		campaignBoard.transform.DOMove (new Vector3 (-0.6f, 0.7f, 0.5f), 1.0f);
		OVRCamera.transform.DOMove (new Vector3 (0, 0.85f, -1.2f), 1.0f);

		UnitsBoard.transform.DOMoveX (-2.2f, 1.0f);
		WeaponsBoard.transform.DOMoveX (2.2f, 1.0f);

		CampaignLobby.SetActive (false);
		MultiLobby.SetActive (false);
		UnitsBoard.SetActive (false);
		WeaponsBoard.SetActive (false);

		if (gpdManager.curr_Select.name == "UnitSelect") {
			gpdManager.curr_Select.transform.DOScale (new Vector3(1.3f,1.3f,1.3f), 1.0f);
		}

		stageChk = true;
		WeaponBoardManager.time_check = true;

		System.Threading.Thread.Sleep (300);
	}

	public void UnitSelectActive(){
		multiBoard.transform.DOMoveX (2.0f, 1.0f);
		campaignBoard.transform.DOMoveX (-2.0f, 1.0f);

		unitSelect.transform.DOScale (new Vector3 (2.5f, 2.5f, 2.5f), 1.0f);
		OVRCamera.transform.DOMove (new Vector3 (0, 0.8f, 0.5f), 1.0f);

		CampaignLobby.SetActive (false);
		UnitsBoard.SetActive (true);
		WeaponsBoard.SetActive (true);

		UnitsBoard.transform.DOMoveX (-2.0f, 1.0f);
		WeaponsBoard.transform.DOMoveX (2.0f, 1.0f);
	}

	IEnumerator SetNonActive() {
		yield return new WaitForSeconds (1.0f);

		multiBoard.SetActive (false);
		campaignBoard.SetActive (false);
	}

	void SceneMove(){
		SceneManager.LoadScene ("03_Multi_Lobby");
	}
}
