using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float speed = .2f;
	private int pickedKeys;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		pickedKeys = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	private void Move() {
		float x;
		float z;

		x = 0;
		z = 0;
		if(Input.GetKey(KeyCode.UpArrow)) {
			z = speed;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			z = -speed;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			x = speed;
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			x = -speed;
		}
		transform.Translate (x, 0, z, Space.World);
	}

	private void Push() {
		
	}

	void OnCollisionEnter(Collision collision) {
		// To manage bullet collision
	}
}
