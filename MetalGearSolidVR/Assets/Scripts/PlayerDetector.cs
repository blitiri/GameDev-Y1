using UnityEngine;
using System;

public class PlayerDetector : MonoBehaviour {
	public GameObject player;
	public float viewingAngle = 90;
	public float maxDetectionDistance = 10;

	protected bool IsPlayerVisible(Transform pointOfView, Vector3 pointOfViewDirection) {
		Vector3 playerDirection;
		float angle;
		bool visible;

		visible = false;
		playerDirection = player.transform.position - pointOfView.position;
		angle = Vector3.Angle(playerDirection, pointOfViewDirection);
		//Debug.Log ("Angle: " + angle + " (" + (viewingAngle / 2) + ")");
		if (angle < (viewingAngle / 2)) {
			// Player is potentially visible, do raycast to check
			visible = Physics.Raycast(pointOfView.position, player.transform.position - pointOfView.position, maxDetectionDistance);
			Debug.DrawRay (pointOfView.position, player.transform.position - pointOfView.position, Color.red, 1);
		}
		return visible;
	}
}
