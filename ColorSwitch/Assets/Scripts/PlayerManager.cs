using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float tapForce = 10;
	public float maxVelocity = 5;
	private Rigidbody playerRB;
	private Vector3 tap;

	void Awake() {
		playerRB = gameObject.GetComponent<Rigidbody> ();
		tap = new Vector3 (0, tapForce, 0);
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isRunning ()) {
			if (Input.GetMouseButton (0)) {
				playerRB.isKinematic = false;
				Tap ();
			}
		}
	}

	private void Tap() {
		playerRB.AddForce (tap, ForceMode.Impulse);
		playerRB.velocity = Vector3.ClampMagnitude (playerRB.velocity, maxVelocity);
	}

	void OnTriggerEnter(Collider other) {
		MeshRenderer wheelRenderer;

		Debug.Log ("Tag: " + other.gameObject.tag);
		// Switch color only once
		if (other.gameObject.tag == "ColorSwitcher") {
			GameManager.instance.SwitchColor ();
			other.gameObject.tag = "ColorSwitcherDisabled";
		}
		else if (other.gameObject.tag == "Wheel") {
			wheelRenderer = other.gameObject.GetComponent<MeshRenderer> ();
			GameManager.instance.WheelCorssing (wheelRenderer.material.color);
		}
		else if (other.gameObject.tag == "Finish") {
			GameManager.instance.Finish ();
		}
	}
}
