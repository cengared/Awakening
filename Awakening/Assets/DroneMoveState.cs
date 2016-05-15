using UnityEngine;
using System.Collections;

// the move state for the Drone AI state machine
public class DroneMoveState : State<DroneAI> {

	// entry into move state
	public void enter(DroneAI drone) {
		Debug.Log ("Enter MoveState");
	}

	// the drone moves between waypoints until the player enters trigger zone
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		drone.move ();
		if (drone.triggered)
			fsm.changeState (new DroneChaseState ());
		
	}

	// exits move state
	public void exit(DroneAI drone) {
		Debug.Log ("Exit MoveState");
	}

}
