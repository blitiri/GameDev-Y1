using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {
	public float destroyAfter = 5.0f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, destroyAfter);
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag.Equals ("Player") || other.gameObject.tag.Equals ("Enemy")) {
			Destroy(other.gameObject);
		}
		Destroy (this.gameObject);
	}
}
