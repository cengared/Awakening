using UnityEngine;
using System.Collections;

// the chase state for DroneAI
public class DroneChaseState : State<DroneAI> {

	public void enter(DroneAI drone) {
		Debug.Log ("Enter ChaseState");

	}
	
	// in chase mode the drone chases the player until they hit then
	// the player is zapped back to area start and drone moves normally
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		drone.chase();
		if (drone.hit) {
			drone.zap ();
			drone.triggered = false;
		}
		if (!drone.triggered) {
			fsm.changeState (new DroneMoveState ());
		}
	}
	
	// at state exit, hit mode is turned off
	public void exit(DroneAI drone) {
		Debug.Log ("Exit ChaseState");
		drone.hit = false;
	
	}
}
