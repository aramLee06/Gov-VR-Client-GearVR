using UnityEngine;
using System.Collections;

public class GamepadConnectionManager : MonoBehaviour {

	public GameObject crossHair;	//Aim
	public GameObject[] UI_Board;
	public Vector3 chng_Scale;

	private int m_uiCheck;
	private float m_expandSize;
	private bool m_joyCheck = true;	//Check joystick Connect or not 
	private GameObject curr_Select;
	private GameObject old_Select;


	void Awake(){
		if (Input.GetJoystickNames ().Length > 0f) {
			Debug.Log ("Connection!");
			m_joyCheck = false;
		} else {
			Debug.Log ("Not Connection!");
		}
		m_expandSize = 0.2f;
		m_uiCheck = 1;

	}	


	void Start () {
		if (m_joyCheck != true) {
			//Aim: Visibla/Invisible
			crossHair.GetComponent<Renderer> ().enabled = m_joyCheck;

			//Initailize Selected Panel
			InintSelect ();
		}
			
	}
	

	void Update () {
	
	}


	private void FixedUpdate(){
		/*if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") < 0) {
		//Move Left Panel using ThumbStick

		if (m_uiCheck < 0)
			m_uiCheck = 0;
		else 
			Invoke("LeftSelect", 0.15f);

		} else if (Input.GetAxis ("Oculus_GearVR_LThumbstickX") > 0) {
			//Move Right Panel using ThumbStick

			if (m_uiCheck > 3)
				m_uiCheck = 3;
			else 
				Invoke("RightSelect", 0.15f);

		} else */if (Input.GetAxis ("Oculus_GearVR_DpadX") < 0) {
			//Move Left Panel using D-Pad

			if (m_uiCheck <= 0) {
				m_uiCheck = 0;
				return;
			} else
				LeftSelect ();//Invoke("LeftSelect", 0.15f);

		} else if (Input.GetAxis ("Oculus_GearVR_DpadX") > 0) {
			//Move Right Panel using D-pad

			if (m_uiCheck >= 3) {
				m_uiCheck = 3;
				return;
			} else
				RightSelect ();//Invoke("RightSelect", 0.15f);

		}
		Time.timeScale = 0.2f;
	}


	private void InintSelect(){
		curr_Select = UI_Board [m_uiCheck];
		old_Select = UI_Board [m_uiCheck];

		chng_Scale = curr_Select.transform.localScale;
		chng_Scale.x += m_expandSize;
		chng_Scale.y += m_expandSize;
		curr_Select.transform.localScale = chng_Scale;
	}


	private void LeftSelect(){
		m_uiCheck--;

		chng_Scale = old_Select.transform.localScale;
		chng_Scale.x -= m_expandSize;
		chng_Scale.y -= m_expandSize;
		old_Select.transform.localScale = chng_Scale;

		curr_Select = UI_Board [m_uiCheck];

		chng_Scale = curr_Select.transform.localScale;
		chng_Scale.x += m_expandSize;
		chng_Scale.y += m_expandSize;
		curr_Select.transform.localScale = chng_Scale;

		old_Select = UI_Board [m_uiCheck];
	}


	private void RightSelect(){
		m_uiCheck++;

		chng_Scale = old_Select.transform.localScale;
		chng_Scale.x -= m_expandSize;
		chng_Scale.y -= m_expandSize;
		old_Select.transform.localScale = chng_Scale;

		curr_Select = UI_Board [m_uiCheck];

		chng_Scale = curr_Select.transform.localScale;
		chng_Scale.x += m_expandSize;
		chng_Scale.y += m_expandSize;
		curr_Select.transform.localScale = chng_Scale;

		old_Select = UI_Board [m_uiCheck];
	}


}
