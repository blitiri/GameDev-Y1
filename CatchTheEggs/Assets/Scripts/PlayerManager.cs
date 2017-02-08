using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float movementSpeed = 200f;
	public float movementIncreasing = 2;
	private Vector3 movement;
	private float leftMovementIncreasing = 0;
	private float rightMovementIncreasing = 0;

	void Awake() {
		movement = new Vector3 (movementSpeed, 0, 0);
	}

	void Update () {
		move ();
	}

	private void move() {
		if(Input.GetKey(KeyCode.D)) {
			if (gameObject.transform.position.x < 4.5) {
				gameObject.transform.Translate (movement * Time.deltaTime + leftMovementIncreasing, Space.World);
				leftMovementIncreasing += movementIncreasing;
				rightMovementIncreasing = 0;
			}
		}
		else if(Input.GetKey(KeyCode.A)) {
			if (gameObject.transform.position.x > -4.5) {
				gameObject.transform.Translate (-movement * Time.deltaTime + rightMovementIncreasing, Space.World);
				rightMovementIncreasing += movementIncreasing;
				leftMovementIncreasing = 0;
			}
		}
	}
}
