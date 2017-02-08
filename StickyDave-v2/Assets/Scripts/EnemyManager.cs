using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public bool moving = false;
	public float jumpSpeed = 5;
	public GameObject startSide;
	private GameObject endSide;
	private Vector3 jumpDirection;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		endSide = startSide;
		jumpDirection = new Vector3(endSide.transform.position.x / Mathf.Abs(endSide.transform.position.x), 0, 0);
		StartJump ();
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			Move ();
		}
	}

	private void Move() {
		transform.Translate (jumpDirection * Time.deltaTime * jumpSpeed);
	}

	private void StartJump() {
		jumpDirection = jumpDirection * -1;
		endSide.transform.position = new Vector3(-endSide.transform.position.x, endSide.transform.position.y, endSide.transform.position.z);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Player")) {
			GameManager.instance.PlayerKilled ();
		}
		if (other.tag.Equals ("Wall")) {
			if (endSide != null) {
				transform.position = endSide.transform.position;
				StartJump ();
			}
		}
	}
}
