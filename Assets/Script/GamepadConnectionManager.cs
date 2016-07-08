using UnityEngine;
using System.Collections;


public class GamepadConnectionManager : MonoBehaviour {

	public GameObject crossHair;	//Aim
	public GameObject[] UI_Board; 	//Option, CampaignBoard, Multi Board, PlayerName
	public GameObject[] UI_SubBoard;//Stage1,2,3, CreateRoom, WaitRoom
	public Vector3 chng_Scale;
	public Vector3 curr_Positon;
	public GameObject curr_Select;
	public GameObject old_Select;
	public int m_BoardLevel;

	private int m_uiCheck;
	private int m_uiSubCheck;
	private int m_stage;	//Sub Board - Campaign Stage
	private int m_chkRoom;	//Sub Board - Multi Room
	private float m_expandSize;
	private bool m_joyCheck = true;	//Check joystick Connect or not 


	void Awake(){

		if (Input.GetJoystickNames ().Length > 0f) {
			Debug.Log ("Connection!");
			m_joyCheck = false;
		} else {
			Debug.Log ("Not Connection!");
		}
		m_expandSize = 0.2f;	//Selected Case
		m_uiCheck = 1;	//UI Board Count
		m_uiSubCheck = 0;	//UI Sub Board Count
		m_BoardLevel=0;
		m_stage = 3;
		m_chkRoom = 2;

	}	


	void Start () {
		if (m_joyCheck != true) {
			//Aim: Visibla/Invisible
			crossHair.GetComponent<Renderer> ().enabled = m_joyCheck;

			//Initailize Selected Panel
			InintSelect ();

		}
			
	}


	void Update(){
		
		if (m_BoardLevel == 0) {
			if (Input.GetAxis ("Oculus_GearVR_DpadX") < 0f) {
				//Move Left Panel using D-Pad

				if (m_uiCheck <= 0) {
					m_uiCheck = 0;

				} else {
					LeftSelect ();
					m_uiCheck--;
					curr_Select = UI_Board [m_uiCheck];
					old_Select = UI_Board [m_uiCheck];
				}
				System.Threading.Thread.Sleep (150);

			} else if (Input.GetAxis ("Oculus_GearVR_DpadX") > 0f) {
				//Move Right Panel using D-pad

				if (m_uiCheck >= 3) {
					m_uiCheck = 3;

				} else {
					//RightSelect ();
				}
				System.Threading.Thread.Sleep (150);
			}
			//Sub Board Clear
			System.Array.Clear (UI_SubBoard, 0, UI_SubBoard.Length);
		
		} else if (m_BoardLevel == 1) {
			//Sub Board
			/*
			switch(m_uiSubCheck){
			//Campaign Board
			case 0:
				UI_SubBoard = new GameObject[m_stage];

				if (Input.GetAxis ("Oculus_GearVR_DpadX") < 0f) {
					//Move Left Panel using D-Pad

					if (m_uiSubCheck <= 0) {
						m_uiSubCheck = 0;

					} else
						LeftSelect ();
					System.Threading.Thread.Sleep (150);

				} else if (Input.GetAxis ("Oculus_GearVR_DpadX") > 0f) {
					//Move Right Panel using D-pad

					if (m_uiSubCheck >= 2) {
						m_uiSubCheck = 2;

					} else
						RightSelect ();
					System.Threading.Thread.Sleep (150);
				}
				break;

			//Multi Board
			case 1:
				UI_SubBoard = new GameObject[m_chkRoom];
				break;
			}
			*/
		} else if (m_BoardLevel == 2) {
			//Campaign Board's Stage
		} else if (m_BoardLevel == 3) {
			//Campaign Board's Rout
		}

	}


	private void InintSelect(){
		//Inintal Current, Old Select
		curr_Select = UI_Board [m_uiCheck];
		old_Select = UI_Board [m_uiCheck];

		//Scale Change
		chng_Scale = curr_Select.transform.localScale;
		chng_Scale.x += m_expandSize;
		chng_Scale.y += m_expandSize;
		curr_Select.transform.localScale = chng_Scale;

		//Debug.Log (curr_Select.name);
	}


	private void LeftSelect(){
		//Array Move Left
		m_uiCheck--;

		//Old Scale Change
		chng_Scale = old_Select.transform.localScale;
		chng_Scale.x -= m_expandSize;
		chng_Scale.y -= m_expandSize;
		old_Select.transform.localScale = chng_Scale;

		//Change Current Select Value
		curr_Select = UI_Board[m_uiCheck];

		//Current Scale Change
		chng_Scale = curr_Select.transform.localScale;
		chng_Scale.x += m_expandSize;
		chng_Scale.y += m_expandSize;
		curr_Select.transform.localScale = chng_Scale;

		//Change Old Select Value
		old_Select = UI_Board[m_uiCheck];

		//Debug.Log (curr_Select.name);
	}


	private void RightSelect(){
		//Array Move Right
		m_uiCheck++;

		//Old Scale Change
		chng_Scale = old_Select.transform.localScale;
		chng_Scale.x -= m_expandSize;
		chng_Scale.y -= m_expandSize;
		old_Select.transform.localScale = chng_Scale;

		//Change Current Select Value
		curr_Select = UI_Board[m_uiCheck];

		//Current Scale Change
		chng_Scale = curr_Select.transform.localScale;
		chng_Scale.x += m_expandSize;
		chng_Scale.y += m_expandSize;
		curr_Select.transform.localScale = chng_Scale;

		//Change Old Scale Value
		old_Select = UI_Board[m_uiCheck];

		//Debug.Log (curr_Select.name);
	}
		
}
