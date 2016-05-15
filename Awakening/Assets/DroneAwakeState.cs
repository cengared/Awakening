using UnityEngine;
using System.Collections;

// the awake state for the Drone AI state machine
public class DroneAwakeState : State<DroneAI> {

	// upon entering the awake state, the drone moves up to awake position
	public void enter(DroneAI drone) {
		Debug.Log ("Enter AwakeState");
		drone.transform.position += new Vector3 (0f, 1.6f, 0f);
		
	}

	// this activates the move state
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		fsm.changeState (new DroneMoveState ());
	}

	// the direction vector is normalised at state exit
	public void exit(DroneAI drone) {
		drone.direction.Normalize ();
		Debug.Log ("Exit AwakeState");
	}
	
}
