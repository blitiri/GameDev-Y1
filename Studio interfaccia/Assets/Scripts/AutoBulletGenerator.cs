using UnityEngine;
using System.Collections;

public class AutoBulletGenerator : MonoBehaviour {
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public float rateOfFire = 3;
	public float fireForce = 300;
	private float timer = 0;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		timer = rateOfFire;
	}

	// Update is called once per frame
	void Update () {
		if(timer < rateOfFire) {
			timer += Time.deltaTime;
		}
		else {
			GameObject bullet = Instantiate (bulletPrefab, spawnPoint.position, spawnPoint.transform.rotation) as GameObject;
			Rigidbody bulletRb = bullet.GetComponent<Rigidbody> ();
			bulletRb.AddForce (spawnPoint.forward * fireForce * Time.deltaTime, ForceMode.Impulse);
			timer = 0;
		}
	}
}
