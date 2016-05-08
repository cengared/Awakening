using UnityEngine;
using System.Collections;

public class AutomaticDoor : MonoBehaviour {

	private Vector3 closedPosition;
	private Vector3 openPosition;
	private bool open;

	// Use this for initialization
	void Start () {
		closedPosition = transform.position;
		openPosition = transform.position + new Vector3(0, 3.25f, 0);
		open = false;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player")
			StartCoroutine (Move ());
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Player")
			StartCoroutine (Move ());
	}

	public IEnumerator Move() {
		Vector3 targetPosition = new Vector3();

		if (!open) {
			targetPosition = openPosition;
		} 
		else {
			targetPosition = closedPosition;
		}

		while (transform.position != targetPosition) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, 0.1f);

			if (Vector3.Distance (transform.position, targetPosition) <= 0.1f) {
				transform.position = targetPosition;
				open = !open;
			}

			yield return null;
		}
	}


}
