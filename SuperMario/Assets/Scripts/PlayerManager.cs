using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float movementSpeed = 10f;
	public float rotationSpeed = 200;
	public float jumpSpeed = 10f;
	public int multiJump = 2;
	public Collider playerCollider;
	public Rigidbody playerRigidbody;
	private bool exults;
	private int multiJumpRemaining;

	// Use this for initialization
	void Start () {
		exults = false;
		multiJumpRemaining = multiJump;
	}
	
	// Update is called once per frame
	void Update () {
		if (exults) {
			Rotate ();
		} else {
			Move ();
		}
	}

	private void Move() {
		int touch;

		if(Input.GetKey(KeyCode.A)) {
			transform.Translate (Vector3.forward * -movementSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D)) {
			transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
		}
		touch = TouchTheGround ();
		if(Input.GetKeyDown(KeyCode.Space) && ((multiJumpRemaining > 0) || (touch > 0))) {
			playerRigidbody.velocity = Vector3.up * jumpSpeed;
			multiJumpRemaining--;
		}
		if(touch > 0) {
			if(Input.GetKey(KeyCode.S)) {
				//Physics.Raycast()
			}
			multiJumpRemaining = multiJump;
			Debug.Log ("Reset multi jump");
		}
	}

	private int TouchTheGround() {
		Vector3 leftRayStart;
		Vector3 rightRayStart;
		RaycastHit hit;
		int touch;

		touch = 0;
		leftRayStart = playerCollider.bounds.center;
		rightRayStart = playerCollider.bounds.center;
		leftRayStart.x -= playerCollider.bounds.extents.x;
		rightRayStart.x += playerCollider.bounds.extents.x;
		Debug.DrawRay (leftRayStart, Vector3.down, Color.red);
		Debug.DrawRay (rightRayStart, Vector3.down, Color.green);
		if (Physics.Raycast (leftRayStart, Vector3.down, out hit, playerCollider.bounds.size.y / 2 + 0.2f)) {
			Debug.Log ("Left touch");
			touch = 1;
		}
		else if (Physics.Raycast (rightRayStart, Vector3.down, playerCollider.bounds.size.y / 2 + 0.2f)) {
			Debug.Log ("Right touch");
			touch = 1;
		}
		else {
			Debug.Log ("No touch");
		}
		return touch;
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag.Equals("Finish")) {
			exults = true;
			GameManager.instance.PlayerWin ();
		}
		else if(other.gameObject.tag.Equals("GroundOfDeath")) {
			GameManager.instance.PlayerDie();
		}
	}

	private void Rotate() {
		gameObject.transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
	}

}
