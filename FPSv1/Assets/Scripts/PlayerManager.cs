using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float movementSpeed = 500.0f;
	public float rotationSpeed = 45.0f;
	private Rigidbody rb;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		float deltaTime;

		deltaTime = Time.deltaTime;
		//deltaTime = 1;
		if(Input.GetKey(KeyCode.A)) {
			transform.RotateAround (transform.position, Vector3.up, -rotationSpeed * deltaTime);
		}
		else if(Input.GetKey(KeyCode.D)) {
			transform.RotateAround (transform.position, Vector3.up, rotationSpeed * deltaTime);
		}
	}

	// FixedUpdate is called once per frame
	void FixedUpdate () {
		float deltaTime;

		deltaTime = Time.deltaTime;
		//deltaTime = 1;
		if(Input.GetKey(KeyCode.S)) {
			rb.AddForce (rb.transform.forward * -movementSpeed * deltaTime);
		}
		else if(Input.GetKey(KeyCode.W)) {
			rb.AddForce (rb.transform.forward * movementSpeed * deltaTime);
		}
	}
}