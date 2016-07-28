using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MLP_Player : MonoBehaviour {

	public Image unitSelectedName;
	public Material[] unitSelectName;

	private int u_idx=0;

	public int UnitIdx{
		get{ return u_idx; }
		set{ u_idx = value; }
	}

	void Update(){
		SelectionChange ();
	}

	public void SelectionChange(){
		unitSelectedName.GetComponent<Image> ().material = unitSelectName[u_idx];
	}
}