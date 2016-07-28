using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

	public AudioSource audioBGM; //You need to insert your BGM to here

	MainLobbyManager lob_Manager;
	TrackingManager trk_Manager;

	public GameObject bgmOn;
	public GameObject bgmOff;

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
			CheckBGM (trk_Manager.trackedItem.name);
			//System.Threading.Thread.Sleep (150);
		}
	}
		
	void Start () {
		trk_Manager = GameObject.Find ("aim").GetComponent<TrackingManager> ();
		audioBGM = GetComponent<AudioSource> ();

		on_Rend = bgmOn.GetComponent<Renderer> ();
		off_Rend = bgmOff.GetComponent<Renderer> ();
	}

	void CheckBGM(string bName){
		switch(bName){
		case "BGMon":
			on_Rend.material.mainTexture = Resources.Load ("On_Button_on") as Texture;
			off_Rend.material.mainTexture = Resources.Load ("off_Button_off") as Texture;
			audioBGM.mute = false;
			break;
		case "BGMoff":
			on_Rend.material.mainTexture = Resources.Load ("On_Button_off") as Texture;
			off_Rend.material.mainTexture = Resources.Load ("off_Button_on") as Texture;
			audioBGM.mute = true;
			break;
		}
	}
}
