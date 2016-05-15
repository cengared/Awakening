using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Leap;

// start scene functionality, based on LeapCharacterControl
public class LeapMenu : MonoBehaviour {
	Controller leapController;
	LeapProvider provider;
	GameObject cam;


	// Use this for initialization
	void Start () {
		leapController = new Controller ();
		Debug.Log ("Leap Motion connected: " + leapController.IsConnected);
		cam = GameObject.Find ("Camera");
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

	// as in LeapCharacterControl
	void Look(Hand h) {
		float rotationThreshold = 60.0f;
		float handX = h.PalmPosition.ToUnityScaled ().x;
		float handY = h.PalmPosition.ToUnityScaled ().y;

		// rotate the player left or right
		if (Mathf.Abs (handX) > rotationThreshold) {
			cam.transform.RotateAround (Vector3.up, (handX/200) * Time.deltaTime);
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
	
	// Update is called once per frame
	void Update () {
		Hand hand = SelectHand ();

		if (hand != null) {
			Look (hand);
			// simple gesture based on hand velocity 
			// used to start the demo proper
			Vector velocity = hand.PalmVelocity;
			if (velocity.y < -1750f) {
				Debug.Log ("Start Demo");
				SceneManager.LoadScene ("level1");
			}
		}

		// quit the demo with escape key
		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
	}
}
