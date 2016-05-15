using UnityEngine;
using System.Collections;

// the sleep state for the Drone AI state machine
public class DroneSleepState : State<DroneAI> {

	// sets drone to it's sleep position
	public void enter(DroneAI drone) {
		Debug.Log ("Enter SleepState");
		drone.transform.position -= new Vector3 (0f, 1.6f, 0f);
	}

	// moves drone state from sleep to awake, controlled by in game trigger
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		if (drone.awake) {
			fsm.changeState (new DroneAwakeState ());
		}
	}

	// exits the sleep state
	public void exit(DroneAI drone) {
		Debug.Log ("Exit SleepState");

	}
	
}
