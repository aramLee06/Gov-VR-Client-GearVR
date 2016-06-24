using UnityEngine;
using System.Collections;

public class TrackingManager : MonoBehaviour {

	public GameObject trackedItem;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		// aiming
		if (Physics.Raycast (this.transform.position, this.transform.forward, out hit)) {
			if (hit.collider.name.CompareTo (this.name) > 0) {
				trackedItem = hit.collider.gameObject;
			} 
		}
	}
}
