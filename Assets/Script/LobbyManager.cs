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

	// Use this for initialization
	void Start () {
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;

		trackingManager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
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
		GameObject.Find ("TextBox").GetComponent<TextMesh> ().text = itemName;

		switch (itemName) {
		case "Campaign_Board": 
			multiBoard.transform.DOMoveX (2.0f, 2.0f);
			break;
		case "Multi_Board":
			campaignBoard.transform.DOMoveX (-2.0f, 2.0f);
			break;
		case "UnitSelect":
			OVRCamera.transform.DOMove (new Vector3 (0, 0.25f, -1.3f), 2.0f);
			break;
		default :
			multiBoard.transform.DOMove (new Vector3 (0.31f, 0.6f, -0.56f), 2.0f);
			campaignBoard.transform.DOMove (new Vector3 (-0.31f, 0.6f, -0.56f), 2.0f);
			OVRCamera.transform.DOMove (new Vector3 (0, 0.6f, -1.7f), 2.0f);
			break;
		}

	}

	void MoveToCampaignLobby() {

	}

}
