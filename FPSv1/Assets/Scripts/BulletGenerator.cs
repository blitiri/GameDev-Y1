using UnityEngine;
using System.Collections;

public class BulletGenerator : MonoBehaviour {
	public GameObject bulletPrefab;
	public Transform spawnPoint;
	public float gunRateOfFire = 3.0f;
	public float tommyGunRateOfFire = .5f;
	public float fireForce = 300;
	private float rateOfFire;
	private float timer = 0;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
		timer = gunRateOfFire;
		rateOfFire = gunRateOfFire;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < rateOfFire) {
			timer += Time.deltaTime;
		} else {
			if (Input.GetMouseButton (0)) {
				GameObject bullet = Instantiate (bulletPrefab, spawnPoint.position, spawnPoint.transform.rotation) as GameObject;
				Rigidbody bulletRb = bullet.GetComponent<Rigidbody> ();
				bulletRb.AddForce (spawnPoint.forward * fireForce * Time.deltaTime, ForceMode.Impulse);
				timer = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag.Equals("Gun")) {
			rateOfFire = gunRateOfFire;
		}
		else if(other.gameObject.tag.Equals("TommyGun")) {
			rateOfFire = tommyGunRateOfFire;
		}
	}
}
