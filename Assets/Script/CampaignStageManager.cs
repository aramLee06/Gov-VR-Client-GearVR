using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CampaignStageManager : MonoBehaviour {

	public GameObject[] stageBoardList;
	GameObject activeStage;
	int currentStage = 0;

	public delegate void OnChangeActiveStageHandler(string stageName);

	public event OnChangeActiveStageHandler OnChangeActiveStage;

	// Use this for initialization
	void Start () {
		SortStageBoard ();
	}
	
	// Update is called once per frame
	void Update () {
	
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

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.Left:
			currentStage++;
			SortStageBoard ();
			break;
		case OVRTouchpad.TouchEvent.Right:
			currentStage--;
			SortStageBoard ();
			break;
		}

	}

	void SortStageBoard() {

		if (currentStage > (stageBoardList.Length - 1)) {
			currentStage = stageBoardList.Length - 1;
			return;
		} else if (currentStage < 0) {
			currentStage = 0;
			return;
		}

		//testText.text = stageBoardList [currentStage].GetComponent<Renderer> ().sortingLayerName;
		for ( int i = 0; i < stageBoardList.Length; i++) {
			if (i == currentStage) {
				stageBoardList [i].GetComponent<Renderer> ().sortingLayerName = "ActiveStage";
				//stageBoardList [i].transform.localScale = new Vector3 (1.0f, 0.6f, 1.0f);
				stageBoardList [i].transform.DOScale (new Vector3 (1.0f, 0.6f, 1.0f), 0.5f);
				OnChangeActiveStage (stageBoardList [i].name);
				continue;
			}

			stageBoardList [i].GetComponent<Renderer> ().sortingLayerName = "OtherStage";
			//stageBoardList [i].transform.localScale = new Vector3 (0.5f, 0.4f, 1.0f);
			stageBoardList [i].transform.DOScale (new Vector3 (0.5f, 0.4f, 1.0f), 0.5f);



		}

	}

}
