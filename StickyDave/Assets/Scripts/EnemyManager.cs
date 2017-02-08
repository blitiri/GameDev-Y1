using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public Transform leftWallTransform;
	public Transform rightWallTransform;
	private Vector3 currDirection;
	private bool moving;

	void Awake() {
		moving = false;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(moving) {
//			Vector3.Lerp ();
		}
	}

	public void setStartDirection(Vector3 startDirection) {
		currDirection = startDirection;
		moving = true;
	}
}
