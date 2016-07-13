using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovingSrc : MonoBehaviour {

	public GameObject unitSelect;

	void Start () {
		//unitSelect = GameObject.Find ("blue_tank_01_ani");
		RotateUnit();
		//unitSelect.transform.DOLocalMoveZ (1.0f, 3.0f);
	}
	
	public void RotateUnit(){
		Vector3 rotateSet = new Vector3 (0, 360.0f, 0);

		unitSelect.transform.DORotate(rotateSet, 25.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
	}
}
