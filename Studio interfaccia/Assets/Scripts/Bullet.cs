using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	//public float speed;
	public float destroyAfter;
	//private Rigidbody rb;

	void Awake() {
		//rb = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, destroyAfter);
	}
	
	// Update is called once per frame
	void Update () {
		//rb.AddForce (Vector3.forward * speed);
	}
}
