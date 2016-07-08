using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PUnitMoveTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("New Path 1"), "time", 20, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop, "orienttopath", true));

    }

    // Update is called once per frame
    void Update () {
        this.transform.Translate (new Vector3 (0.0f, 0.0f, 0.5f) * Time.deltaTime);
        this.transform.Rotate (new Vector3 (0.0f,Input.GetAxis ("Horizontal")*Mathf.Rad2Deg, 0.0f) * Time.deltaTime);

        if (Input.GetKey (KeyCode.Escape)) {
			SceneManager.LoadScene ("02_Lobby_new");
		}
		//this.transform.Translate (new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * Time.deltaTime);
		//Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")*
	}
}
