using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// for the end game trigger
public class EndTrigger : MonoBehaviour {
	public Text text;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			text.text = "This is the end of the demo. Exiting the trigger area will exit the application.";
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			text.text = "";
			Application.Quit();
		}
	}
}
