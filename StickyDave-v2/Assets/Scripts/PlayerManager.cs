using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Wall")) {
			GameManager.instance.StopJump ();
		}
	}
}
