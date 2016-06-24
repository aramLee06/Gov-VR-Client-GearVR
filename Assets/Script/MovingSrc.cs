using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovingSrc : MonoBehaviour {

	public GameObject untiSelect;
	// Use this for initialization
	void Start () {

		float offset = 0.10f;
		int duration = 5;
		Vector3 rotateSet = new Vector3 (0, 360.0f, 0);

		//untiSelect.transform.DOLocalMoveY (offset, duration).SetLoops (-1, LoopType.Yoyo).SetRelative (true);
		//untiSelect.transform.DOBlendableRotateBy(rotateSet, 25.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
		untiSelect.transform.DORotate(rotateSet, 25.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetRelative(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
