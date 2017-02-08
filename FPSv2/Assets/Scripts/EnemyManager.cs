using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject target;
	public float movementSpeed = 10;
	public int points = 10;
	private bool movingEnabled;
	public GameObject barrel;

	// Use this for initialization
	void Start () {
		movingEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (movingEnabled) {
			Move ();
		} else {
			GameManager.instance.Fire (barrel.transform, 1);
		}
	}

	private void Move() {
		gameObject.transform.LookAt(target.transform, Vector3.up);
		gameObject.transform.Translate(gameObject.transform.forward * Time.deltaTime * movementSpeed, Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			movingEnabled = false;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			movingEnabled = false;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			movingEnabled = true;
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag.Equals ("Bullet")) {
			GameManager.instance.EnemyKilled (gameObject, points);
		}
	}
}
