using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// handles the functionality of the green trigger
// zone, based on the AutomaticDoor class
public class GreenTrigger : MonoBehaviour {

	GameObject rightDoor, leftDoor;
	Vector3 leftStart, rightStart;
	public Text text;
	bool opened;

	// Use this for initialization
	void Start () {
		rightDoor = GameObject.Find ("GreenRight");
		leftDoor = GameObject.Find ("GreenLeft");
		rightStart = rightDoor.transform.position;
		leftStart = leftDoor.transform.position;
		opened = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (!opened) {
				text.text = "Green Door Opened";
				opened = true;
				StartCoroutine (Move ());
			}
			// sends a message to the drones to stop chasing
			GameObject[] drones = GameObject.FindGameObjectsWithTag ("Drone");
			foreach (GameObject d in drones)
				d.SendMessage ("ResetTrigger");
			
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			text.text = "";
		}
	}
		
	public IEnumerator Move() {
		Vector3 rightTarget = rightStart + new Vector3 (-1.3f, 0f, 0f);
		Vector3 leftTarget = leftStart + new Vector3 (1.3f, 0f, 0f);
			
		while (rightDoor.transform.position != rightTarget) {
			rightDoor.transform.position = Vector3.MoveTowards (rightDoor.transform.position, rightTarget, 0.05f);
			leftDoor.transform.position = Vector3.MoveTowards (leftDoor.transform.position, leftTarget, 0.05f);

			if (Vector3.Distance (rightDoor.transform.position, rightTarget) <= 0.05f) {
				rightDoor.transform.position = rightTarget;
			}

			yield return null;
		}
	}
}
