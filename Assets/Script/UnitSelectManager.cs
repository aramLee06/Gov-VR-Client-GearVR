using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class UnitSelectManager : MonoBehaviour {

	TrackingManager trackingManager;

	public GameObject[] unitBoardList;

	void Start(){

		trackingManager = GameObject.Find ("Aim").GetComponent<TrackingManager> ();

		unitBoardList [3].SetActive (false);
		unitBoardList [5].SetActive (false);
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
			CheckSelectUnits (trackingManager.trackedItem.name);
			break;
		}
	}

	void CheckSelectUnits(string unitName){
		switch (unitName) {
		case "Unit_Tank":
			unitBoardList [0].transform.DOScale (new Vector3(0.45f, 0.15f, 1.0f), 1.0f);
			unitBoardList [3].SetActive (false);
			unitBoardList [5].SetActive (false);
			break;
		case "Unit_Dron":
			unitBoardList [1].transform.DOScale (new Vector3(0.45f, 0.15f, 1.0f), 1.0f);
			unitBoardList [2].SetActive (false);
			unitBoardList [4].SetActive (false);
			break;
		}
	}
}
