using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AirDBaordManager : MonoBehaviour {

	TrackingManager trk_Manager;
	MainLobbyManager lob_Manager;

	public GameObject[] airD;
	public int a_Chk;

	Renderer[] rend = new Renderer[4];

	void OnEnable(){

		lob_Manager = GameObject.Find ("GameManager").GetComponent<MainLobbyManager> ();
		lob_Manager.OnTapObject += OnTapObject;
	}

	void OnDisable(){

		lob_Manager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string weapName)
	{
		if (weapName != null) {
			CheckSelectAirD (trk_Manager.trackedItem.name);
			//System.Threading.Thread.Sleep (150);
		}
	}

	void Start () {
		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		a_Chk = 4;

		for (int i = 0; i < 4; i++) {
			rend [i] = airD[i].GetComponent<Renderer> ();
		}

		for (int j = 0; j < 4; j++) {
			rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
			if (j == 0) {
				rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
				airD [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
			} else {
				airD [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
			}
		}
	}

	void CheckSelectAirD(string airdName){
		switch (airdName) {
		case "A1name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 0) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					airD [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					airD [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			a_Chk = 4;
			break;
		case "A2name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 1) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					airD [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					airD [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			a_Chk = 5;
			break;
		case "A3name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 2) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					airD [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					airD [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			a_Chk = 6;
			break;
		case "A4name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 3) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					airD [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					airD [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			a_Chk = 7;
			break;
		}
	}
		
}
