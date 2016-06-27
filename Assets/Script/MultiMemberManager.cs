using UnityEngine;
using System.Collections;

public class MultiMemberManager : MonoBehaviour {

	public PlayerMemberSetting playerMemberSetting;
	public GameObject[] UnitList;

	void OnEnable(){
		playerMemberSetting.OnChangePlayerMemberCount += OnChangePlayerMemberCount;
	}


	void OnDisable() {
		playerMemberSetting.OnChangePlayerMemberCount -= OnChangePlayerMemberCount;
	}

	void OnChangePlayerMemberCount(int cnt) {

		switch (cnt) {
		case 1: 
			this.transform.localScale = new Vector3 (0.1f, 0.4f, 1.0f);
			this.transform.localPosition = new Vector3(0.0f, -0.27f, 0.0f);
			break;
		case 2:
			this.transform.localScale = new Vector3 (0.1f, 0.8f, 1.0f);
			this.transform.localPosition = new Vector3(0.0f, -0.37f, 0.0f);
			break;

		case 3:
			this.transform.localScale = new Vector3 (0.1f, 1.1f, 1.0f);
			this.transform.localPosition = new Vector3(0.0f, -0.47f, 0.0f);
			break;

		case 4:
			this.transform.localScale = new Vector3 (0.1f, 1.5f, 1.0f);
			this.transform.localPosition = new Vector3(0.0f, -0.6f, 0.0f);
			break;
		}
		SetActivition (cnt);
	}

	void SetActivition(int cnt) {
		for (int i = 0; i < UnitList.Length; i++) {
			Debug.Log (i + " / " + cnt);
			if (i < cnt) {
				UnitList [i].SetActive (true);
			} else {
				UnitList [i].SetActive (false);
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () {
	
	}
}
