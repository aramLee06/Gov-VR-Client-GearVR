using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class UnitBoardManager : MonoBehaviour {

	TrackingManager trk_manager;
	LobbyManager lob_Manager;

	public GameObject tanks;
	public GameObject airD;
	public GameObject tankBoard;
	public GameObject airDBoard;

	public Renderer rend1;
	public Renderer rend2;

	public delegate void OnTapObjectHandler(string stageName);
	//public event OnTapObjectHandler OnTapObject;

	// Use this for initialization
	void Start () {

		trk_manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		rend1 = tanks.GetComponent<Renderer> ();
		rend2 = airD.GetComponent<Renderer> ();

		rend1.material.mainTexture = Resources.Load ("Tank_Unit_on") as Texture;
		rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_off") as Texture;

		tankBoard.SetActive (true);
		airDBoard.SetActive (false);
	}
	
	void OnEnable(){
		OVRTouchpad.Create ();
		OVRTouchpad.TouchHandler += GearTouchHandler;

		lob_Manager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		lob_Manager.OnTapObject += OnTapObject;
	}

	void OnDisable(){
		OVRTouchpad.TouchHandler -= GearTouchHandler;
		lob_Manager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string unitName)
	{

	}

	void GearTouchHandler(object sender, System.EventArgs e){
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.SingleTap:
			CheckSelectUnit (trk_manager.trackedItem.name);
			System.Threading.Thread.Sleep (150);
			break;
		}
	}

	public void CheckSelectUnit(string unitName){
		switch (unitName) {
		case "Tanks":
			rend1.material.mainTexture = Resources.Load ("Tank_Unit_on") as Texture;
			rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_off") as Texture;

			tankBoard.SetActive (true);
			airDBoard.SetActive (false);
			break;

		case "AirDrons":
			rend1.material.mainTexture = Resources.Load ("Tank_Unit_off") as Texture;
			rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_on") as Texture;

			tankBoard.SetActive (false);
			airDBoard.SetActive (true);
			break;

		default :
			OnTapObject (unitName);
			break;
		}
	}
}
