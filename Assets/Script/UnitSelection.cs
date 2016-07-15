using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitSelection : MonoBehaviour {
	
	UnitBoardManager ub_Manager;

	private List<GameObject> units;

	void Start () {
		ub_Manager = GameObject.Find ("UnitBoard").GetComponent<UnitBoardManager> ();

		units = new List<GameObject> ();

		foreach(Transform t in this.transform){
			if (t.gameObject.name != "Spotlight" && t.gameObject.name != "UnitBoard" && t.gameObject.name != "WeaponBoard"/* && t.gameObject.name.Substring(7) != "missile"*/) {
				units.Add (t.gameObject);
				t.gameObject.SetActive (false);
			}
		}
		Selections ();
	}

	void Update(){
		Selections ();
	}

	public void Selections(){
		if (ub_Manager.unitChk == true) {
			for (int i = 0; i < 8; i++) {
				if (i == GameObject.Find ("TankUnits").GetComponent<TankBoardManager> ().t_Chk) {
					units [GameObject.Find ("TankUnits").GetComponent<TankBoardManager> ().t_Chk].SetActive (true);
				} else {
					units [i].SetActive (false);
				}
			}
		} else if (ub_Manager.unitChk == false) {
			for (int j = 0; j < 8; j++) {
				if (j == GameObject.Find("AirDUnits").GetComponent<AirDBaordManager>().a_Chk) {
					units [GameObject.Find("AirDUnits").GetComponent<AirDBaordManager>().a_Chk].SetActive (true);
				} else {
					units [j].SetActive (false);
				}
			}
		}
	}
}
