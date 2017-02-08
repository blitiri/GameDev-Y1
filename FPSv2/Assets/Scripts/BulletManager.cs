using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {
	public float bulletLifeTime = 2;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletLifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
