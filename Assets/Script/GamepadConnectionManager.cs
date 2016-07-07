using UnityEngine;
using System.Collections;

public class LobbyUI_Anim{
	public AnimationClip Cmp_B;
	public AnimationClip Mlt_B;
	public AnimationClip Ply_B;
	public AnimationClip Opt_B;
}

public class GamepadConnectionManager : MonoBehaviour {

	public GameObject crossHair;	//Aim
	public GameObject cmp_Board;	//Campaign Board
	public GameObject mlt_Board;	//Multi Board
	public GameObject opt_Board;	//Option Board
	public GameObject plyr_name;	//Player Name Board
	public GameObject old_Select;	
	//public Vector3 chng_Scale;
	public LobbyUI_Anim anim;
	public Animation _anima;

	private bool m_joyCheck = true;	//Check joystick Connect or not 


	void Awake(){
		if (Input.GetJoystickNames ().Length > 0f) {
			Debug.Log ("Connection!");
			m_joyCheck = false;
		} else {
			Debug.Log ("Not Connection!");
		}
	}	


	void Start () {
		if (m_joyCheck != true) {
			//CrossHair.SetActive (m_joyCheck);
			crossHair.GetComponent<Renderer> ().enabled = m_joyCheck;

			//Initialize Animation
			/*
			_anima.GetComponentInChildren<Animation> ();
			_anima.clip = anim.Highlighted;
			_anima.Play ();
			*/

			//Change selected board's scale
			/*	--TEST--
			chng_Scale = cmp_Board.transform.localScale;
			chng_Scale.x += 0.2f;
			cmp_Board.transform.localScale = chng_Scale;
			*/

		}
	}
	

	void Update () {
	
	}
}
