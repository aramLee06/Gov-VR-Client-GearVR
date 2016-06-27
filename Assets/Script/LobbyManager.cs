using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class LobbyManager : MonoBehaviour {

	TrackingManager trackingManager;

	public GameObject campaignBoard;
	public GameObject multiBoard;
	public GameObject unitSelect;
	public GameObject OVRCamera;

	public GameObject CampaignLobby;
	public GameObject MultiLobby;
	//public GameObject UnitSelectLobby;

	// Use this for initialization
	void Start () {
		trackingManager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		CampaignLobby.SetActive (false);
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
		
	}

	void CheckSelectedMode(string itemName) {

		GameObject.Find ("TestText").GetComponent<TextMesh> ().text = itemName;
		switch (itemName) {
		case "Campaign_Board": 
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);
			CampaignLobby.SetActive (true);
			break;
		case "Multi_Board":
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);
			MultiLobby.SetActive (true);
			break;
		case "UnitSelect":
			OVRCamera.transform.DOMove (new Vector3 (0, 0.45f, 0.2f), 2.0f);
			break;
	
		default :
			multiBoard.transform.DOMove (new Vector3 (0.4f, 0.85f, 2.0f), 2.0f);
			campaignBoard.transform.DOMove (new Vector3 (-0.4f, 0.85f, 2.0f), 2.0f);
			OVRCamera.transform.DOMove (new Vector3 (0, 0.85f, -0.73f), 2.0f);
			CampaignLobby.SetActive (false);
			MultiLobby.SetActive (true);
			break;
		}

	}

}
