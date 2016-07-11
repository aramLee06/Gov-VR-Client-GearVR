// No Use Anymore

using UnityEngine;
using System.Collections;
using DG.Tweening;


public class GamepadConnectionManager : MonoBehaviour {

	LobbyManager lb_Manager;
	public GameObject[] UI_Board; 	//Option, CampaignBoard, UnitSelect, Multi Board, PlayerName
	public GameObject curr_Select;
	public GameObject old_Select;

	private int m_uiCheck;

	void Start () {
		lb_Manager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		m_uiCheck = 2;
		//Initailize Selected Panel
		InintSelect ();
	}
		
	void Update(){
		if (lb_Manager.stageChk == true) {
			if (Input.GetAxis ("Oculus_GearVR_DpadX") < 0f) {
				//Move Left Panel using D-Pad

				if (m_uiCheck <= 0) {
					m_uiCheck = 0;

				} else {
					LeftSelect ();
				}
				System.Threading.Thread.Sleep (150);

			} else if (Input.GetAxis ("Oculus_GearVR_DpadX") > 0f) {
				//Move Right Panel using D-pad

				if (m_uiCheck >= 4) {
					m_uiCheck = 4;

				} else {
					RightSelect ();
				}
				System.Threading.Thread.Sleep (150);
			}
		}
	}


	private void InintSelect(){
		//Inintal Current, Old Select
		curr_Select = UI_Board [m_uiCheck];
		old_Select = UI_Board [m_uiCheck];

		//Scale Change
		curr_Select.transform.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.5f);
		//Debug.Log (curr_Select.name);
	}


	private void LeftSelect(){
		//Array Move Left
		m_uiCheck--;

		//Old Scale Change
		if (old_Select.name == "UnitSelect") {
			old_Select.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 0.5f);
		} else {
			old_Select.transform.DOScale(new Vector3(0.7f,0.7f,0.7f), 0.5f);
		}

		//Change Current Select Value
		curr_Select = UI_Board[m_uiCheck];

		//Current Scale Change
		if (curr_Select.name == "UnitSelect") {
			curr_Select.transform.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.5f);
		} else {
			curr_Select.transform.DOScale (new Vector3 (0.9f, 0.9f, 0.9f), 0.5f);
		}

		//Change Old Select Value
		old_Select = UI_Board[m_uiCheck];

		//Debug.Log (curr_Select.name);
	}


	private void RightSelect(){
		//Array Move Right
		m_uiCheck++;

		//Old Scale Change
		if (old_Select.name == "UnitSelect") {
			old_Select.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 0.5f);
		} else {
			old_Select.transform.DOScale(new Vector3(0.7f,0.7f,0.7f), 0.5f);
		}


		//Change Current Select Value
		curr_Select = UI_Board[m_uiCheck];

		//Current Scale Change
		if (curr_Select.name == "UnitSelect") {
			curr_Select.transform.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.5f);
		} else {
			curr_Select.transform.DOScale (new Vector3 (0.9f, 0.9f, 0.9f), 0.5f);
		}
		//Change Old Scale Value
		old_Select = UI_Board[m_uiCheck];

		//Debug.Log (curr_Select.name);
	}
		
}
