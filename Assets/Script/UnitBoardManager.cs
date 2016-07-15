using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class UnitBoardManager : MonoBehaviour {

	TrackingManager trk_Manager;
	LobbyManager lob_Manager;
	CampaignStageManager cs_Manager;
	WeaponBoardManager weap_Manager;

	public GameObject tanks;
	public GameObject airD;
	public GameObject tankBoard;
	public GameObject airDBoard;
	public GameObject tankWBoard;
	public GameObject airDWBoard;

	public bool unitChk;

	Renderer rend1;
	Renderer rend2;

	void Start () {

		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		cs_Manager = GameObject.Find ("CampaignLobbyBoard").GetComponent<CampaignStageManager> ();
		weap_Manager = GameObject.Find ("WeaponBoard").GetComponent<WeaponBoardManager> ();

		rend1 = tanks.GetComponent<Renderer> ();
		rend2 = airD.GetComponent<Renderer> ();

		SettingUnitBoard ();
	}

	void Update(){
		SettingUnitBoard ();
	}
	
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
		}
	}

	public void SettingUnitBoard(){
		if (cs_Manager.currentStage == 0) {
			unitChk = true;

			rend1.material.mainTexture = Resources.Load ("Tank_Unit_on") as Texture;
			rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_off") as Texture;

			tankBoard.SetActive (true);
			airDBoard.SetActive (false);
			tankWBoard.SetActive (true);
			airDWBoard.SetActive (false);
		} else if (cs_Manager.currentStage == 1) {
			unitChk = false;

			rend1.material.mainTexture = Resources.Load ("Tank_Unit_off") as Texture;
			rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_on") as Texture;

			tankBoard.SetActive (false);
			airDBoard.SetActive (true);
			tankWBoard.SetActive (false);
			airDWBoard.SetActive (true);
		}
	}

	public void CheckSelectUnit(string unitName){
		switch (unitName) {
		case "Tanks":
			if (cs_Manager.currentStage == 0) {
				rend1.material.mainTexture = Resources.Load ("Tank_Unit_on") as Texture;
				rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_off") as Texture;

				unitChk = true;

				if (weap_Manager.weapChk == true) {
					tankWBoard.SetActive (true);
					airDWBoard.SetActive (false);
				}
				tankBoard.SetActive (true);
				airDBoard.SetActive (false);
			}
			break;

		case "AirDrons":
			if (cs_Manager.currentStage == 1) {
				rend1.material.mainTexture = Resources.Load ("Tank_Unit_off") as Texture;
				rend2.material.mainTexture = Resources.Load ("Aircraft_Unit_on") as Texture;

				unitChk = false;

				if (weap_Manager.weapChk == true) {
					tankWBoard.SetActive (false);
					airDWBoard.SetActive (true);
				}

				tankBoard.SetActive (false);
				airDBoard.SetActive (true);
			}
			break;
			/*
		default :
			OnTapObject (unitName);
			break;
			*/
		}
	}
}
