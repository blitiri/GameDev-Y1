using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	public GameObject target;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		FollowTarget ();
	}

	private void Init() {
		Vector3 cameraPosition;
		Transform targetTransform;

		targetTransform = target.transform;
		cameraPosition = new Vector3 (targetTransform.position.x, targetTransform.position.y, 10);
		gameObject.transform.position = cameraPosition;
		gameObject.transform.LookAt (target.transform, Vector3.up);
	}

	private void FollowTarget() {
		Vector3 cameraPosition;
		Vector3 targetPosition;

		targetPosition = target.transform.position;
		cameraPosition = new Vector3 (targetPosition.x, targetPosition.y, gameObject.transform.position.z);
		gameObject.transform.position = cameraPosition;
	}
}
