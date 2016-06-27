using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovingSrc : MonoBehaviour {

	public GameObject unitSelect;
	// Use this for initialization
	void Start () {

		float offset = 0.10f;
		int duration = 5;
		Vector3 rotateSet = new Vector3 (0, 360.0f, 0);

		//unitSelect.transform.DORotate(rotateSet, 25.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetRelative(true);

		unitSelect.transform.DOLocalMoveZ (1.0f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
