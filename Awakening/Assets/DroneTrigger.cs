using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroneTrigger : MonoBehaviour {
	public Text text;
	bool allActivated = false;

	// wakes up all the drones in the scene
	void OnTriggerEnter(Collider other) {
		GameObject[] drones = GameObject.FindGameObjectsWithTag ("Drone");
		if (!allActivated) {
			if (other.gameObject.tag == "Player") {
				foreach (GameObject d in drones) {
					DroneAI dAI = d.GetComponent<DroneAI> ();
					dAI.awake = true;
				}
				allActivated = true;
				text.text = "Drones Activated";
			}
		} else {
			// sends a message to the drones to stop chasings
			drones = GameObject.FindGameObjectsWithTag ("Drone");
			foreach (GameObject d in drones)
				d.SendMessage ("ResetTrigger");
		}
	}

	// reset text on trigger exit
	void OnTriggerExit(Collider other)
	{
		text.text = "";
	}
}
