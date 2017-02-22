using UnityEngine;
using System.Collections;

public class CameraManager : PlayerDetector {
	public GameObject lens;
	public float maxAngleRotation = 90;
	public float rotationSpeed = 2f;
	private IList reacheableEnemies;
	private bool alarmSended;
	private bool rotate;
	private Vector3 startRotation;
	private Vector3 endRotation;
	private Vector3 currentAngle;
	private Vector3 targetAngle;
	private bool forward;

	void Awake() {
		Vector3 initAngle;

		reacheableEnemies = new ArrayList ();
		alarmSended = false;
		rotate = true;
		initAngle = gameObject.transform.eulerAngles;
		startRotation = new Vector3(initAngle.x, -(maxAngleRotation / 2), initAngle.z) + gameObject.transform.parent.eulerAngles;
		endRotation = new Vector3(initAngle.x, maxAngleRotation / 2, initAngle.z) + gameObject.transform.parent.eulerAngles;
		currentAngle = startRotation;
		targetAngle = endRotation;
		transform.Rotate (startRotation);
		forward = true;
	}

	void Update () {
		Move ();
		LookingForPlayer ();
	}

	private void LookingForPlayer() {
		EnemyManager enemyManager;

		if (IsPlayerVisible (lens.transform, lens.transform.up)) {
			if (!alarmSended) {
				Debug.Log ("Alarm send");
				foreach (GameObject enemy in reacheableEnemies) {
					enemyManager = enemy.GetComponent<EnemyManager> ();
					enemyManager.ReactToAllarm (gameObject);
				}
				alarmSended = true;
			}
			rotate = false;
		} else {
			if (alarmSended) {
				Debug.Log ("Alarm ceased");
				foreach (GameObject enemy in reacheableEnemies) {
					enemyManager = enemy.GetComponent<EnemyManager> ();
					enemyManager.AlarmCeased ();
				}
				alarmSended = false;
			}
			rotate = true;
		}
	}

	private void Move() {
		float lerpStep;

		if (rotate) {
			lerpStep = (Time.deltaTime * rotationSpeed) / Vector3.Angle (currentAngle, targetAngle);
			currentAngle = new Vector3 (
				currentAngle.x,
				Mathf.LerpAngle (currentAngle.y, targetAngle.y, lerpStep),
				currentAngle.z);
			transform.eulerAngles = currentAngle;
			if (Vector3.Angle (currentAngle, targetAngle) < 0.1f) {
				forward = !forward;
				if (forward) {
					currentAngle = startRotation;
					targetAngle = endRotation;
				} else {
					currentAngle = endRotation;
					targetAngle = startRotation;
				}
			}
//		} else {
//			transform.LookAt (player.transform);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Enemy")) {
			reacheableEnemies.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("Enemy")) {
			reacheableEnemies.Remove (other.gameObject);
		}
	}
}
