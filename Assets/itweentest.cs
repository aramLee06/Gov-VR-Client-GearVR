using UnityEngine;
using System.Collections;

public class itweentest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("New Path 1"), "time", 20, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop, "orienttopath", true));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
