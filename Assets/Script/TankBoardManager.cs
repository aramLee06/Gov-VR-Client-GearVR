using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TankBoardManager : MonoBehaviour {
	
	TrackingManager trk_Manager;
	LobbyManager lob_Manager;

	public GameObject[] tank;
	public int t_Chk;

	Renderer[] rend = new Renderer[4];

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
			CheckSelectTank (trk_Manager.trackedItem.name);
			//System.Threading.Thread.Sleep (150);
		}
	}
		
	void Start () {
		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		t_Chk = 0;

		for (int i = 0; i < 4; i++) {
			rend [i] = tank[i].GetComponent<Renderer> ();
		}

		for (int j = 0; j < 4; j++) {
			rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
			if (j == 0) {
				rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
				tank [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
			} else {
				tank [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
			}
		}
	}

	void CheckSelectTank(string tankName){
		switch (tankName) {
		case "T1name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 0) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					tank [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					tank [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			t_Chk = 0;
			break;
		case "T2name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 1) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					tank [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					tank [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			t_Chk = 1;
			break;
		case "T3name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 2) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					tank [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					tank [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			t_Chk = 2;
			break;
		case "T4name":
			for (int j = 0; j < 4; j++) {
				rend [j].material.mainTexture = Resources.Load ("Unit_Name_Base_off") as Texture;
				if (j == 3) {
					rend [j].material.mainTexture = Resources.Load	("Unit_Name_Base_on") as Texture;
					tank [j].transform.DOScale (new Vector3 (1.1f, 0.42f, 0.3f), 0.3f);
				} else {
					tank [j].transform.DOScale (new Vector3 (1.0f, 0.38f, 0.3f), 0.3f);
				}
			}
			t_Chk = 3;
			break;
		}
	}

}
