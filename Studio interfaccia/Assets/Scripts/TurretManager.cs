using UnityEngine;
using System.Collections;

public class TurretManager : MonoBehaviour {
	public float rotationSpeed = 10;
	public Transform target;
	private float lastTargetX;
	private float lastTargetZ;

	// Use this for initialization
	void Start () {
		lastTargetX = 0;
		lastTargetZ = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetDirection;

		if((lastTargetX != target.transform.position.x) || (lastTargetZ != target.transform.position.z)) {
			transform.LookAt (target, Vector3.up);
			lastTargetX = target.transform.position.x;
			lastTargetZ = target.transform.position.z;
		}
	}
}
