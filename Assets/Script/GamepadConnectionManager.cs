using UnityEngine;
using System.Collections;
using DG.Tweening;
using VR = UnityEngine.VR;

public class GamepadConnectionManager : MonoBehaviour {

	LobbyManager lb_Manager;
	public GameObject[] UI_Board; 	//Option, CampaignBoard, UnitSelect, Multi Board, PlayerName
	public GameObject curr_Select;
	public GameObject old_Select;

	public delegate void OnTapObjectHandler(string stageName);

	private int m_uiCheck;

	Renderer c_Rend;
	Renderer m_Rend;

	void Start () {
		lb_Manager = GameObject.Find ("GameManager").GetComponent<LobbyManager> ();
		m_uiCheck = 1;

		c_Rend = GameObject.Find ("Campaign_Board").GetComponent<Renderer>();
		m_Rend = GameObject.Find ("Multi_Board").GetComponent<Renderer> ();
		//Initailize Selected Panel
		InintSelect ();
	}

	void OnEnable() {
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;
	}

	void OnDisable() {
		OVRTouchpad.TouchHandler -= GearTouchHandler;
	}

	void GearTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		if (lb_Manager.stageChk == true) {
			switch (touchArgs.TouchType) {
			case OVRTouchpad.TouchEvent.Right:
				if (m_uiCheck <= 0) {
					m_uiCheck = 0;

				} else {
					LeftSelect ();
				}
				break;
			case OVRTouchpad.TouchEvent.Left:
				if (m_uiCheck >= 3) {
					m_uiCheck = 3;

				} else {
					RightSelect ();
				}
				break;
			}
		}
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

				if (m_uiCheck >= 3) {
					m_uiCheck = 3;

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
		if (curr_Select.name == "Campaign_Board") {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_on") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		} else if (curr_Select.name == "Multi_Board") {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_on") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		} else {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		}
		//Debug.Log (curr_Select.name);
	}


	private void LeftSelect(){
		//Array Move Left
		m_uiCheck--;

		//Old Scale Change
		old_Select.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 0.5f);

		//Change Current Select Value
		curr_Select = UI_Board[m_uiCheck];

		//Current Scale Change
		if (curr_Select.name == "Campaign_Board") {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_on") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		} else if (curr_Select.name == "Multi_Board") {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_on") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		} else {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		}

		//Change Old Select Value
		old_Select = UI_Board[m_uiCheck];

		//Debug.Log (curr_Select.name);
	}


	private void RightSelect(){
		//Array Move Right
		m_uiCheck++;

		//Old Scale Change
		old_Select.transform.DOScale(new Vector3(1.0f,1.0f,1.0f), 0.5f);


		//Change Current Select Value
		curr_Select = UI_Board[m_uiCheck];

		//Current Scale Change
		if (curr_Select.name == "Campaign_Board") {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_on") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		} else if (curr_Select.name == "Multi_Board") {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_on") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		} else {
			c_Rend.material.mainTexture = Resources.Load ("Campaign_Main_off") as Texture;
			m_Rend.material.mainTexture = Resources.Load ("Multiplay_Main_off") as Texture;
			curr_Select.transform.DOScale (new Vector3 (1.2f, 1.2f, 1.2f), 0.5f);
		}
		//Change Old Scale Value
		old_Select = UI_Board[m_uiCheck];

		//Debug.Log (curr_Select.name);
	}
		
}
