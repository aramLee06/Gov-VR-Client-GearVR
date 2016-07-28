using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource audioSound; //You need to insert your Sound to here

	MainLobbyManager lob_Manager;
	TrackingManager trk_Manager;

	public GameObject sdOn;
	public GameObject sdOff;

	Renderer on_Rend;
	Renderer off_Rend;

	void OnEnable(){
		lob_Manager = GameObject.Find ("GameManager").GetComponent<MainLobbyManager> ();
		lob_Manager.OnTapObject += OnTapObject;
	}

	void OnDisable(){
		lob_Manager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string bName)
	{
		if (bName != null) {
			CheckSound (trk_Manager.trackedItem.name);
			//System.Threading.Thread.Sleep (150);
		}
	}

	void Start () {
		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();

		on_Rend = sdOn.GetComponent<Renderer> ();
		off_Rend = sdOff.GetComponent<Renderer> ();
	}

	void CheckSound(string bName){
		switch (bName) {
		case "Sdon":
			on_Rend.material.mainTexture = Resources.Load ("On_Button_on") as Texture;
			off_Rend.material.mainTexture = Resources.Load ("off_Button_off") as Texture;
			audioSound.mute = false;
			break;
		case "Sdoff":
			on_Rend.material.mainTexture = Resources.Load ("On_Button_off") as Texture;
			off_Rend.material.mainTexture = Resources.Load ("off_Button_on") as Texture;
			audioSound.mute = true;
			break;
		}
	}
}
