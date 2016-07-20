using UnityEngine;
using System.Collections;

public class LayerSortingOnBoard : MonoBehaviour {

	//public Renderer StageBoard;
	public Renderer Campaign01;
	public Renderer Campaign02;
	public Renderer Campaign03;

	MainLobbyManager lobbyManager; 

	CampaignStageManager campaignStageManager;

	void OnEnable(){
		campaignStageManager = GameObject.Find ("CampaignLobbyBoard").GetComponent<CampaignStageManager>();
		campaignStageManager.OnChangeActiveStage += OnChangeActiveStage;
		lobbyManager = GameObject.Find ("GameManager").GetComponent<MainLobbyManager> ();
		lobbyManager.OnTapObject += OnTapObject;
	}

	void OnDisable() {
		campaignStageManager.OnChangeActiveStage -= OnChangeActiveStage;
		lobbyManager.OnTapObject -= OnTapObject;
	}

	void OnTapObject (string stageName)
	{

	}

	void OnChangeActiveStage (string stageName)
	{
		if (this.name == stageName) {
			Campaign01.sortingLayerName = "ActiveCampaign";
			Campaign02.sortingLayerName = "ActiveCampaign";
			Campaign03.sortingLayerName = "ActiveCampaign";
			this.GetComponent<Renderer> ().sortingLayerName = "ActiveStage";
		} else {
			Campaign01.sortingLayerName = "OtherCampaign";
			Campaign02.sortingLayerName = "OtherCampaign";
			Campaign03.sortingLayerName = "OtherCampaign";
			this.GetComponent<Renderer> ().sortingLayerName = "OtherStage";
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
