using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider other) {
		if(other.tag.Equals("Player")) {
			transform.LookAt (other.transform, Vector3.up);
		}
	}
}
