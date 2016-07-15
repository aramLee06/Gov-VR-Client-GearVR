using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class WeaponBoardManager : MonoBehaviour {

	TrackingManager trk_Manager;
	LobbyManager lob_Manager;
	UnitBoardManager uni_Manager;
	CampaignStageManager cs_Manager;

	//public GameObject weapOnOff;
	public GameObject tankOnOff;
	public GameObject airdOnOff;
	public GameObject[] weap;

	public bool weapChk;

	//Renderer onoffRend;
	Renderer[] rend = new Renderer[6];

	private int w_Chk;
	private List<GameObject> weapons;

	void OnEnable(){
		lob_Manager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		lob_Manager.OnTapObject += OnTapObject;
	}

	void OnDisable(){
		lob_Manager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string weapName)
	{
		if (weapName != null) {
			CheckSelectWeap (trk_Manager.trackedItem.name);
			//System.Threading.Thread.Sleep (150);
		}
	}

	void Start(){
		
		weapChk = true;
		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		uni_Manager = GameObject.Find ("UnitBoard").GetComponent<UnitBoardManager> ();
		cs_Manager = GameObject.Find ("CampaignLobbyBoard").GetComponent<CampaignStageManager> ();

		//onoffRend = weapOnOff.GetComponent<Renderer> ();
		//onoffRend.material.mainTexture = Resources.Load ("weapon_button_on") as Texture;

		weapons = new List<GameObject> ();

		foreach(Transform t in this.transform){
			if (t.gameObject.name != "Weapons" && t.gameObject.name != "TankWeapons" && t.gameObject.name != "AirDWeapons") {
				weapons.Add (t.gameObject);
				t.gameObject.SetActive (false);
			}
		}

		for (int i = 0; i < 6; i++) {
			rend[i] = weap[i].GetComponent<Renderer> ();
		}

		for (int j = 0; j < 6; j++) {
			rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
			if (j == 0) {
				rend [j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
				weap [j].transform.DOScale (new Vector3 (1.1f, 0.5f, 0.3f), 0.3f);
				if (cs_Manager.currentStage == 0) {
					weapons [j].SetActive (true);
				} else if(cs_Manager.currentStage == 1){
					weapons [j].SetActive (false);
				}
			} else {
				weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
			}
		}
	}

	void Update(){
		for (int j = 0; j < 6; j++) {
			rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
			if (j == 0) {
				rend [j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
				weap [j].transform.DOScale (new Vector3 (1.1f, 0.5f, 0.3f), 0.3f);
				if (cs_Manager.currentStage == 0) {
					weapons [j].SetActive (true);
				} else if(cs_Manager.currentStage == 1){
					weapons [j].SetActive (false);
				}
			} else {
				weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
			}
		}
	}
		
	void CheckSelectWeap(string weapName){
		switch (weapName) {
		case "W1name":
			for (int j = 0; j < 6; j++) {
				rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
				if (j == 0) {
					rend [j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
					weap [j].transform.DOScale (new Vector3(1.1f, 0.5f,0.3f), 0.3f);
					weapons [j].SetActive (true);
				}else {
					weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
					weapons [j].SetActive (false);
				}
			}
			break;
		case "W2name":
			for (int j = 0; j < 6; j++) {
				rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
				if (j == 1) {
					rend[j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
					weap [j].transform.DOScale (new Vector3(1.1f, 0.5f,0.3f), 0.3f);
					weapons [j].SetActive (true);
				}else {
					weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
					weapons [j].SetActive (false);
				}
			}
			break;
		case "W3name":
			for (int j = 0; j < 6; j++) {
				rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
				if (j == 2) {
					rend [j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
					weap [j].transform.DOScale (new Vector3(1.1f, 0.5f,0.3f), 0.3f);
					weapons [j].SetActive (true);
				}else {
					weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
					weapons [j].SetActive (false);
				}
			}
			break;
		case "W4name":
			for (int j = 0; j < 6; j++) {
				rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
				if (j == 3) {
					rend[j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
					weap [j].transform.DOScale (new Vector3(1.1f, 0.5f,0.3f), 0.3f);
				}else {
					weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
				}
			}
			break;
		case "W5name":
			for (int j = 0; j < 6; j++) {
				rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
				if (j == 4) {
					rend[j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
					weap [j].transform.DOScale (new Vector3(1.1f, 0.5f,0.3f), 0.3f);
				}else {
					weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
				}
			}
			break;
		case "W6name":
			for (int j = 0; j < 6; j++) {
				rend [j].material.mainTexture = Resources.Load ("weapon_Name_Base_off") as Texture;
				if (j == 5) {
					rend[j].material.mainTexture = Resources.Load	("weapon_Name_Base_on") as Texture;
					weap [j].transform.DOScale (new Vector3(1.1f, 0.5f,0.3f), 0.3f);
				}else {
					weap [j].transform.DOScale (new Vector3 (1.0f, 0.45f, 0.3f), 0.3f);
				}
			}
			break;
			/*
		case "Weapons":
			if (weapChk == true) {
				onoffRend.material.mainTexture = Resources.Load ("weapon_select_off") as Texture;
				tankOnOff.SetActive (false);
				airdOnOff.SetActive (false);
				weapChk = false;
			} else {
				onoffRend.material.mainTexture = Resources.Load ("weapon_button_on") as Texture;
				if (uni_Manager.unitChk == true) {
					tankOnOff.SetActive (true);
					airdOnOff.SetActive (false);
				} else {
					tankOnOff.SetActive (false);
					airdOnOff.SetActive (true);
				}
				weapChk = true;
			}
			break;

		default:
			OnTapObject (weapName);
			break;
			*/
		}
	}
}

