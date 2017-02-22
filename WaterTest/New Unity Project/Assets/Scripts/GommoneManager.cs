using UnityEngine;
using System.Collections;

public class GommoneManager : MonoBehaviour {
	public Rigidbody gommoneRB;
	public float bias = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		gommoneRB.AddForce (collision.gameObject.transform.right * bias);
	}
}
