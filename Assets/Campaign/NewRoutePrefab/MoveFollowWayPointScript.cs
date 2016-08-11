using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

[RequireComponent(typeof(WaypointProgressTracker))]
public class MoveFollowWayPointScript : MonoBehaviour {

	private WaypointProgressTracker tracker = null;

	[SerializeField, Range(0, 10)]
	protected float speed = 1;

	//private Transform[] wayPoints;

	// Use this for initialization
	void Start () {
		tracker = GetComponent<WaypointProgressTracker> ();
		//WaypointCircuit circuit = GameObject.Find ("Manager").GetComponent<RouteMovingManager> ().circuit;

		//wayPoints = circuit.Waypoints;

		//tracker.SetCircuit (circuit);
	}
	
	// Update is called once per frame
	void Update () {

		if (tracker.currentNum == tracker.lastWaypoint) {
			return;
		}
		Vector3 targetPosition = tracker.progressPoint.position + tracker.progressPoint.direction;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, speed * Time.deltaTime);
		transform.LookAt (targetPosition);
	}
}
