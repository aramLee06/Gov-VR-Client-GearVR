using UnityEngine;
using System.Collections;

public class SetLayerName : MonoBehaviour {

	public string layerName;
	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer> ().sortingLayerName = layerName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
