using UnityEngine;
using System.Collections;
using Leap;

[RequireComponent(typeof (CharacterController))]

public class LeapCharacterControl : MonoBehaviour {

	Controller leapController;
	GameObject player, cam;
	bool handOpenThisFrame, handOpenLastFrame;
	CharacterController characterController;

	// Use this for initialization
	void Start () {
		leapController = new Controller ();
		player = GameObject.FindGameObjectWithTag("Player");
		cam = GameObject.Find ("Camera");
		handOpenThisFrame = false;
		handOpenLastFrame = false;
		characterController = GetComponent<CharacterController> ();
	}

	Hand SelectHand() {
		Frame f = leapController.Frame ();
		Hand nearestHand = null;
		float zMax = -float.MaxValue;
		for (int i = 0; i < f.Hands.Count; i++) {
			float palmZ = f.Hands [i].PalmPosition.ToUnityScaled ().z;
			if (palmZ > zMax) {
				zMax = palmZ;
				nearestHand = f.Hands [i];
			}
		}
		return nearestHand;
	}

	bool IsHandOpen(Hand h) {
		return h.Fingers.Count > 1;
	}

	void Look(Hand h) {
		float rotationThreshold = 120.0f;
		float handX = h.PalmPosition.ToUnityScaled ().x;
		float handY = h.PalmPosition.ToUnityScaled ().y;

		// rotate the player left or right
		if (Mathf.Abs (handX) > rotationThreshold) {
			player.transform.RotateAround (Vector3.up, (handX/200) * Time.deltaTime);
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

	void Move(Hand h) {
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		// moves the player forwards if hand movement is more than the dead zone threshold
		if (h.PalmPosition.ToUnityScaled ().z < -25.0f) {
			characterController.SimpleMove (forward * 5f);
		}

		// moves the player backwards if hand movement is more than the dead zone threshold
		if (h.PalmPosition.ToUnityScaled ().z > 200) {
			characterController.SimpleMove (-forward * 2.5f);
		}

	}

	void Update () {
		Hand hand = SelectHand ();

		if (hand != null) {
			handOpenThisFrame = IsHandOpen (hand);
			Look (hand);
			Move (hand);
		}
		handOpenLastFrame = handOpenThisFrame;
	}

}
