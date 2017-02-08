using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject bulletPrefab;
	public GameObject enemyPrefab;
	public float maxBulletSpeed = 10f;
	public int score;
	private bool gameActive;

	void Awake() {
		instance = this;
		score = 0;
		gameActive = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Fire(Transform spawn, float speedFactor) {
		GameObject bullet;
		Vector3 bulletPosition;
		Rigidbody bulletRigidbody;
//		Quaternion spawnRotation;
//		Quaternion bulletRotation;
//		Vector3 newBulletRotation;

		bullet = Instantiate (bulletPrefab) as GameObject;
		bulletPosition = spawn.position;
		bullet.transform.position = new Vector3 (bulletPosition.x + 1, bulletPosition.y, bulletPosition.z); 

//		spawnRotation = spawn.transform.rotation;
//		bulletRotation = bullet.transform.rotation;
//		Debug.Log ("spawnRotation.eulerAngles.z: " + spawnRotation.eulerAngles.z + " - bulletRotation.eulerAngles.y: " + bulletRotation.eulerAngles.y);
//		newBulletRotation = new Vector3 (0, 0, spawnRotation.eulerAngles.z - bulletRotation.eulerAngles.y);
//		bullet.transform.Rotate (newBulletRotation , Space.World);

		bullet.transform.rotation = Quaternion.FromToRotation (spawn.up, bullet.transform.forward);

		bulletRigidbody = bullet.GetComponent<Rigidbody> ();
		bulletRigidbody.AddForce (bullet.transform.up * maxBulletSpeed * speedFactor, ForceMode.Impulse);
	}

	public void EnemyKilled(GameObject enemy, int points) {
		score += points;
		SpawnEnemies (enemy, 2);
		Debug.Log ("Score: " + score);
	}

	private void SpawnEnemies(GameObject spawn, int instances) {
		GameObject enemy;
		Vector3 enemyPosition;
		Vector3 spawnPosition;
		int instance;

		for (instance = 0; instance < instances; instance++) {
			spawnPosition = spawn.transform.position;
			enemyPosition = new Vector3 (spawnPosition.x + 1 * instance, spawnPosition.y, spawnPosition.z);
			enemy = Instantiate (enemyPrefab) as GameObject;
			enemy.transform.position = enemyPosition;
		}
	}

	public void PlayerKilled() {
		Debug.Log ("Looser!!!");
		gameActive = false;
	}

	public bool IsGameActive() {
		return gameActive;
	}
}
