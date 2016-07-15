using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSelection : MonoBehaviour {

	WeaponBoardManager wb_Manager;
	CampaignStageManager cs_Manager;

	private List<GameObject> weapons;

	void Start () {
		wb_Manager = GameObject.Find ("WeaponBoard").GetComponent<WeaponBoardManager> ();

		weapons = new List<GameObject> ();

		foreach(Transform t in this.transform){
			if (t.gameObject.name != "Weapons" && t.gameObject.name != "TankWeapons" && t.gameObject.name != "AirDWeapons") {
				weapons.Add (t.gameObject);
				t.gameObject.SetActive (false);
			}
		}


	}

	void Update () {
	
	}

	public void Selections(){
		if (cs_Manager.currentStage == 0) {
			for (int i = 0; i < 3; i++) {
				if (i == GameObject.Find ("TankUnits").GetComponent<TankBoardManager> ().t_Chk) {
					weapons [GameObject.Find ("TankUnits").GetComponent<TankBoardManager> ().t_Chk].SetActive (true);
				} else {
					weapons [i].SetActive (false);
				}
			}
		}/* else if (wb_Manager.unitChk == false) {
			for (int j = 0; j < 8; j++) {
				if (j == GameObject.Find("AirDUnits").GetComponent<AirDBaordManager>().a_Chk) {
					weapons [GameObject.Find("AirDUnits").GetComponent<AirDBaordManager>().a_Chk].SetActive (true);
				} else {
					weapons [j].SetActive (false);
				}
			}
		} */
	}
}
