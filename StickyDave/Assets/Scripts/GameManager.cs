using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public float gravityIntensity = 2;
	public int pathLength = 20;

	void Awake() {
		Physics.gravity = Vector3.right * gravityIntensity;
	}

	// Use this for initialization
	void Start () {
		GeneratePath ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space)) {
			Debug.Log ("Invert gravity");
			Physics.gravity = Physics.gravity * -1;
		}
	}

	private void GeneratePath() {
		int pathIndex;


		for(pathIndex = 0; pathIndex < pathLength; pathIndex++) {
			
		}
	}
}
 