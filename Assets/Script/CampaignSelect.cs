using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CampaignSelect : MonoBehaviour {

	MainLobbyManager lobbyManager; 

	void OnEnable() {
		lobbyManager = GameObject.Find ("GameManager").GetComponent<MainLobbyManager> ();
		lobbyManager.OnTapObject += OnTapObject;
	}

	void OnDisable() {
		lobbyManager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string stageName)
	{
		if (this.name == stageName) {
			//SceneManager.LoadScene ("04_temp_InGame");
			GameObject.Find("GameManager").SendMessage("UnitSelectActive");
		}
		//GameObject.Find ("TestText").GetComponent<TextMesh> ().text = stageName;
	}
}
