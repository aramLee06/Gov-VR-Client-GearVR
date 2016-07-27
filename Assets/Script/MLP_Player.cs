using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MLP_Player : MonoBehaviour {

	public Image unitSelectedName;
	public Material[] unitSelectName;

	public int idx=0;

	void Update(){

	}

	public void SelectionChange(){
		unitSelectedName.GetComponent<Image> ().material = unitSelectName[idx];
	}
}
