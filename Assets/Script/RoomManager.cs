using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {
	LobbyManager lob_Manager;
	TrackingManager trk_Manager;

	public GameObject UnitModel;
	public GameObject[] multi_unit;
	public GameObject[] multi_unitBoard;

	Renderer[] unit_rend = new Renderer[8];

	void OnEnable(){
		lob_Manager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		lob_Manager.OnTapObject += OnTapObject;
	}

	void OnDisable(){
		lob_Manager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string unitName)
	{
		if (unitName != null) {
			CheckSelectUnit (trk_Manager.trackedItem.name);
			//Debug.Log (trk_Manager.trackedItem.name);
		}
	}

	void Start () {
		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();

		for (int i = 0; i < 8; i++) {
			unit_rend [i] = multi_unitBoard [i].GetComponent<Renderer> ();
		}

	}

	void Update(){
		if (lob_Manager.stageChk == true) {
			for (int i = 0; i < 8; i++) {
				multi_unit [i].SetActive (false);
				unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
			}
		}
	}

	void CheckSelectUnit(string unitName){
		switch(unitName){
		case "B1name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 0) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "B2name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 1) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "B3name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 2) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "B4name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 3) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "R1name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 4) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "R2name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 5) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "R3name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 6) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		case "R4name":
			//UnitModel.SetActive (true);
			for (int i = 0; i < 8; i++) {
				if (i == 7) {
					multi_unit [i].SetActive (true);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_off")as Texture;
				} else {
					multi_unit [i].SetActive (false);
					unit_rend [i].material.mainTexture = Resources.Load ("Unit_Name_Base_on")as Texture;
				}
			}
			break;
		}
	}
}
