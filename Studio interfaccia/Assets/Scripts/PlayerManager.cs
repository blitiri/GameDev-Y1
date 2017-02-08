using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float movementSpeed = 500.0f;
	public float rotationSpeed = 45.0f;
	public float jumpSpeed = 200.0f;
	public bool useDeltaTime = false;
	public float stopAfter = 0.0f;
	private Rigidbody rb;
//	private bool stopped;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
//		rb.AddForce (Vector3.forward * speed);
//		stopped = false;
	}

	// Update is called once per frame
	void Update () {
		float deltaTime;

		deltaTime = Time.deltaTime;
		//deltaTime = 1;
		if(Input.GetKey(KeyCode.A)) {
			transform.RotateAround (transform.position, Vector3.up, -rotationSpeed * deltaTime);
			//			rb.AddForce (Vector3.right * -speed * deltaTime);
		}
		else if(Input.GetKey(KeyCode.D)) {
			transform.RotateAround (transform.position, Vector3.up, rotationSpeed * deltaTime);
			//			rb.AddForce (Vector3.right * speed * deltaTime);
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
//		if(Input.GetKey(KeyCode.Space)) {
//			rb.AddForce (Vector3.up * jump * deltaTime);
//		}
//		if(!stopped && (Time.time >= stopAfter)) {
//			rb.AddForce (Vector3.forward * -speed);
//			stopped = true;
//		}

	}
}
