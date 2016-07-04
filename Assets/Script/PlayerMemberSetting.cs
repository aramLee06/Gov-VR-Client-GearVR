using UnityEngine;
using System.Collections;

public class PlayerMemberSetting : MonoBehaviour {


	public TextMesh redMemberCnt;
	public TextMesh blueMemberCnt;
	int memberCnt = 1;

	public GameObject MultiSelectBoard;

	public delegate void OnChangePlayMemberCountHandler(int cnt);

	public event OnChangePlayMemberCountHandler OnChangePlayerMemberCount;

	void OnEnable() {
		OVRTouchpad.Create();
		OVRTouchpad.TouchHandler += GearTouchHandler;

		MultiSelectBoard.SetActive (false);
	}

	void OnDisable() {
		OVRTouchpad.TouchHandler -= GearTouchHandler;
		MultiSelectBoard.SetActive (true);
	}
	void GearTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

		switch (touchArgs.TouchType) {
		case OVRTouchpad.TouchEvent.Up:
			CountUp ();
			break;
		case OVRTouchpad.TouchEvent.Down:
			CountDown ();
			break;
		}
	}

	void CountUp() {

		if (memberCnt >= 4) {
			return;
		}

		memberCnt++;
		redMemberCnt.text = memberCnt.ToString ();
		blueMemberCnt.text = memberCnt.ToString ();
		OnChangePlayerMemberCount (memberCnt);
	}

	void CountDown() {
		
		if (memberCnt <= 1) {
			return;
		}

		memberCnt--;
		redMemberCnt.text = memberCnt.ToString ();
		blueMemberCnt.text = memberCnt.ToString ();
		OnChangePlayerMemberCount (memberCnt);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
