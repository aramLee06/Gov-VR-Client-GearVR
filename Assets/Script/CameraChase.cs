using UnityEngine;
using System.Collections;

public class CameraChase : MonoBehaviour {

	public Transform Unit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Unit != null) {
			this.transform.position = new Vector3 (Unit.position.x, Unit.position.y, Unit.position.z);
			//this.transform.Rotate (new Vector3 (0.0f, Unit.rotation.y, 0.0f));
			this.transform.rotation = new Quaternion (this.transform.rotation.x, Unit.rotation.y, this.transform.rotation.z, Unit.rotation.w);
		}
	}
}
