using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof (CharacterController))]

public class LeapCharacterControl : MonoBehaviour {

	Controller leapController;
	GameObject player, cam;
	CharacterController characterController;

	// Use this for initialization
	void Start () {
		leapController = new Controller ();
		player = GameObject.FindGameObjectWithTag("Player");
		cam = GameObject.Find ("Camera");
		characterController = GetComponent<CharacterController> ();
		Cursor.visible = false;
	}

	// returns the hand that is furthest from the player (closest to the screen)
	Hand SelectHand() {
		Frame frame = leapController.Frame ();
		Hand furthestHand = null;
		float zMax = float.MaxValue;
		for (int i = 0; i < frame.Hands.Count; i++) {
			float palmZ = frame.Hands [i].PalmPosition.ToUnityScaled ().z;
			//Debug.Log ("PalmZ: " + palmZ);
			if (palmZ < zMax) {
				zMax = palmZ;
				furthestHand = frame.Hands [i];
			}
		}
		return furthestHand;
	}

	// handles functionality for looking around the scene
	void Look(Hand h) {
		float rotationThreshold = 120.0f;
		float handX = h.PalmPosition.ToUnityScaled ().x;
		float handY = h.PalmPosition.ToUnityScaled ().y;

		// rotate the player left or right
		if (Mathf.Abs (handX) > rotationThreshold) {
			player.transform.Rotate (Vector3.up, (handX/4) * Time.deltaTime);
		}

		// rotate view down
		if (handY < 150f && cam.transform.rotation.x < 0.35f) {
			cam.transform.Rotate (1f, 0f, 0f);
		}

		// rotate view up
		if (handY > 300f && cam.transform.rotation.x > -0.35f) {
			cam.transform.Rotate (-1f, 0f, 0f);
		}
	}

	// movement functionality using a dead zone 
	void Move(Hand h) {
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		// moves the player forwards if the hand moves out of dead zone
		if (h.PalmPosition.ToUnityScaled ().z < 0f) {
			characterController.SimpleMove (forward * 6f);
		}

		// moves the player backwards if the hand moves out of dead zone
		if (h.PalmPosition.ToUnityScaled ().z > 200f) {
			characterController.SimpleMove (-forward * 3f);
		}
	}

	void Update () {
		// get the hand that is nearest to the screen
		Hand hand = SelectHand (); 

		if (hand != null) {
			Look (hand); 
			Move (hand); 
		}

		// quit the demo with escape key
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}
}
