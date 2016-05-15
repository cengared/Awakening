using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// the Agent AI behaviours
public class DroneAI : MonoBehaviour {

	// holds the state machine
	StateMachine<DroneAI> fsm;
	// public variables for a particular drone
	public Vector3[] waypoints = new Vector3[3];
	public Text text;
	// variables for controlling the AI
	public Vector3 direction { get; set; }
	public bool triggered { get; set; }
	public bool awake { get; set; }
	public bool hit { get; set; }
	float moveSpeed;
	float rotateSpeed;
	Vector3 sendPlayerHere;
	int nextWaypoint;
	GameObject player;

	public DroneAI() {
		fsm = new StateMachine<DroneAI> (this);
		fsm.setState (new DroneSleepState ()); // starts the agent in the sleepstate
		moveSpeed = 4f;
		rotateSpeed = 8f;
		sendPlayerHere = new Vector3 (0f, 0f, 50f);
		hit = false;
	}

	void Start () {
		player = GameObject.Find ("Player");
		setNextWaypoint ();
	}

	void Update () {
		fsm.update ();
	}
			
	// the move function sends the drone from waypoint to waypoint
	public void move() {
		Vector3 a = transform.position;
		Vector3 b = waypoints [nextWaypoint];
		rotateTowards (b);
		float aToB = Vector3.Distance (a, b);
		float soFar = (Time.deltaTime) * moveSpeed;
		float done = soFar / aToB;
		transform.position = Vector3.Lerp (a, b, done);
		if (transform.position == b) {
			setNextWaypoint ();
			Debug.Log ("Next waypoint is: " + nextWaypoint);
		}
	}

	// this is for chasing the player when they've hit the trigger zone
	public void chase() {
		Vector3 a = transform.position;
		Vector3 b = player.transform.position;
		rotateTowards (b);
		float aToB = Vector3.Distance (a, b);
		float soFar = (Time.deltaTime) * (moveSpeed * 2);
		float done = soFar / aToB;
		transform.position = Vector3.Lerp (a, b, done);
		Debug.Log (aToB);
		if (aToB < 2.1f) {
			hit = true;
			aToB = float.MaxValue;
		}
	}

	// zaps the player back to the start of the drone area
	public void zap() {
		text.text = "Drone Zapped You!";
		player.transform.position = sendPlayerHere;
		hit = false;
	}

	// rotates the drone towards a passed target vector
	void rotateTowards(Vector3 target) {
		Vector3 targetDirection = target - transform.position;
		float step = rotateSpeed * Time.deltaTime;
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDirection);
	}

	// set what the next waypoint will be based on current waypoint
	void setNextWaypoint() {
		nextWaypoint++;
		if (nextWaypoint > waypoints.Length - 1)
			nextWaypoint = 0;
	}

	// checks the trigger zone of the drone
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
			triggered = true;
	}

	// receives a message from the trigger zone stopping the drone's 
	// chase mode if it's activated when hitting the trigger
	void ResetTrigger() {
		triggered = false;
	}
}
