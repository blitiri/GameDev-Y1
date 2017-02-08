using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject player;
	public GameObject mainCamera;
	public float cameraSpeed = .5f;
	private PlayerManager playerManager;
	private float lastCameraMovement;

	void Awake() {
		playerManager = player.GetComponent<PlayerManager>();
		lastCameraMovement = 0;
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		CheckPlayerDied ();
	}

	private void CheckPlayerDied() {
		Rigidbody2D plyerRigidbody;

		if(player.transform.position.y < -5) {
			plyerRigidbody = player.GetComponent<Rigidbody2D> ();
			plyerRigidbody.isKinematic = true;
		}
	}

	void LateUpdate() {
		Vector3 destCameraPosition;
		Vector3 cameraPosition;
		float distance;

		cameraPosition = mainCamera.transform.position;
		destCameraPosition = playerManager.GetCameraPosition ();
		// Preserve camera Z coord
		destCameraPosition.z = cameraPosition.z;
		distance = Vector3.Distance (cameraPosition, destCameraPosition);
		lastCameraMovement += (distance / cameraSpeed);
		mainCamera.transform.position = Vector3.Lerp (cameraPosition, destCameraPosition, lastCameraMovement * Time.deltaTime);
	}
}
