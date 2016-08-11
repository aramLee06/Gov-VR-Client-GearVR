using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class RouteMovingManager : MonoBehaviour {

	public GameObject[] RouteType;
	public GameObject[] UnitType;
	public WaypointCircuit circuit;
	Transform StartSpot;

	int routeIdx = -1;
	int unitIdx = -1;

	public GameObject CurrentRoute {
		get { return RouteType [routeIdx]; }
	}

	void Awake() {
		for (int i = 0; i < 4; i++) {
			if (i < 3) {
				RouteType [i].SetActive (false);
			}
			UnitType [i].SetActive (false);
		}
	}
	// Use this for initialization
	void Start () {
		routeIdx = -1;
		unitIdx = -1;

	}

	// Update is called once per frame
	void Update () {
	
	}

	void SetRoute() {

		//Instantiate(RouteType[routeIdx], RouteType[routeIdx].transform.position, RouteType[routeIdx].transform.rotation);
		RouteType[routeIdx].SetActive(true);

		circuit = RouteType [routeIdx].GetComponent<WaypointCircuit> ();

		StartSpot = circuit.Waypoints [0];
		StartSpot.rotation = circuit.Waypoints [0].rotation;
		Debug.Log (StartSpot.rotation = circuit.Waypoints [0].rotation);
		StartSpot.position = new Vector3 (circuit.Waypoints[0].position.x, 0.0f, circuit.Waypoints[0].position.z);
	}

	void SetUnit() {
		//Instantiate (UnitType [unitIdx], StartSpot.position, StartSpot.rotation);
		UnitType[unitIdx].SetActive(true);
		UnitType [unitIdx].transform.position = StartSpot.position;
		UnitType [unitIdx].transform.rotation = StartSpot.rotation;
		GameObject.Find ("CameraContainer").GetComponent<CameraChase> ().Unit = UnitType [unitIdx].transform;
		//UnitType [unitIdx].GetComponent<MoveFollowWayPointScript> ().SetRoute (UnitType [unitIdx]);
	}

	void OnGUI()
	{
		if (routeIdx < 0)
		{
			if (GUI.Button(new Rect(20, 40, 80, 20), "루트 A"))
			{
				routeIdx = 0;
				SetRoute ();
			}
			if (GUI.Button(new Rect(20, 70, 80, 20), "루트 B"))
			{
				routeIdx = 1;
				SetRoute ();
			}
			if (GUI.Button(new Rect(20, 100, 80, 20), "루트 C"))
			{
				routeIdx = 2;
				SetRoute ();
			}
		}
		else if (unitIdx < 0)
		{
			if (GUI.Button(new Rect(20, 40, 80, 20), "블루 탱크 1"))
			{
				unitIdx = 0;
				SetUnit ();
			}
			if (GUI.Button(new Rect(20, 70, 80, 20), "블루 탱크 2"))
			{
				unitIdx = 1;
				SetUnit ();
			}
			if (GUI.Button(new Rect(20, 100, 80, 20), "레드 탱크 1"))
			{
				unitIdx = 2;
				SetUnit ();
			}
			if (GUI.Button(new Rect(20, 130, 80, 20), "레드 탱크 2"))
			{
				unitIdx = 3;
				SetUnit ();
			}
		}
	}
}
