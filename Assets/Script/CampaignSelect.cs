using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CampaignSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	LobbyManager lobbyManager; 

	void OnEnable(){
		lobbyManager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		lobbyManager.OnTapObject += OnTapObject;
	}

	void OnDisable() {
		lobbyManager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string stageName)
	{
		if (this.name == stageName) {
			SceneManager.LoadScene ("04_temp_InGame");
		}
		//GameObject.Find ("TestText").GetComponent<TextMesh> ().text = stageName;
	}
}
