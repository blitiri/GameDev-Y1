using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Move(Vector3 direction) {
		Rigidbody rb;

		rb = gameObject.GetComponent<Rigidbody> ();
		rb.AddForce (direction * 400);
	}
}
