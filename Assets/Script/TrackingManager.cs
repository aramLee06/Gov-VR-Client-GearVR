using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TrackingManager : MonoBehaviour {

	public GameObject trackedItem;
	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer> ().sortingLayerName = "LobbyAim";
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		// aiming
		if (Physics.Raycast (new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.01f), this.transform.forward, out hit)) {
			trackedItem = hit.collider.gameObject;
		}
			
	}
}
