using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	public PlayerManager playerManager;
	public float cameraSpeed = .5f;
	private float lastCameraMovement = 0;

	void LateUpdate() {
		Vector3 destCameraPosition;
		Vector3 cameraPosition;
		float distance;

		if (playerManager) {
			cameraPosition = transform.position;
			destCameraPosition = playerManager.GetCameraPivotPosition ();
			// Preserve camera Z coord
			destCameraPosition.z = -1;//cameraPosition.z;
			distance = Vector3.Distance (cameraPosition, destCameraPosition);
			lastCameraMovement += (distance / cameraSpeed);
			transform.position = Vector3.Lerp (cameraPosition, destCameraPosition, lastCameraMovement * Time.deltaTime);
		}
	}
}
