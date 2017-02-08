using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject camera;
	public GameObject player;
	public GameObject enemyPrefab;
	public GameObject modulePrefab;
	public GameObject SpawnPointPrefab;
	public int pathLength = 20;
	public int minEnemyDistance = 2;
	public GameObject startSide;
	public float jumpSpeed = 5;
	public int noEnemyProb = 70;
	public int stoppedEnemyProb = 20;
	public int movingEnemyProb = 10;
	public int minFreeSteps = 3;
	public int startLength = 10;
	private int maxProb;
	private GameObject endSide;
	private Vector3 jumpDirection;
	private bool jumping;
	private bool running;
	private int lastEnemyPos = 0;
	private bool init;

	void Awake() {
		instance = this;
		running = true;
		init = true;
	}

	// Use this for initialization
	void Start () {
		endSide = startSide;
		maxProb = noEnemyProb + stoppedEnemyProb + movingEnemyProb;
		jumping = false;
		jumpDirection = new Vector3(endSide.transform.position.x / Mathf.Abs(endSide.transform.position.x), 0, 0);
		player.transform.position = endSide.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(init) {
			StartCoroutine(GeneratePath());
			init = false;
		}
		if (running) {
			MovePlayer ();
		}
	}

	public void PlayerKilled() {
		running = false;
		Destroy (player);
		Debug.Log ("You Loose!!!");
	}

	public void StopJump() {
		jumping = false;
		player.transform.position = endSide.transform.position;
	}

	private void MovePlayer() {
		camera.transform.Translate (Vector3.up * Time.deltaTime);
		player.transform.Translate (Vector3.up * Time.deltaTime);
		endSide.transform.Translate (Vector3.up * Time.deltaTime);
		if (!jumping && Input.GetKey (KeyCode.Space)) {
			jumping = true;
			jumpDirection = jumpDirection * -1;
			endSide.transform.position = new Vector3(-endSide.transform.position.x, endSide.transform.position.y, endSide.transform.position.z);
		}
		if (jumping) {
			player.transform.Translate (jumpDirection * Time.deltaTime * jumpSpeed);
		}
		if (player.transform.position.y > pathLength) {
			running = false;
			Debug.Log ("You Win!!!");
		}
	}

	private IEnumerator GeneratePath() {
		GameObject module = null;
		GameObject enemy = null;
		GameObject spawnPoint = null;
		EnemyManager enemyManager = null;
		int posIndex;
		int prob;
		int side;

		for (posIndex = 0; ; posIndex++) {
			module = Instantiate (modulePrefab) as GameObject;
			module.transform.position = new Vector3 (0, posIndex, 0);
			if ((posIndex > minFreeSteps) && ((posIndex - lastEnemyPos) > minEnemyDistance)) {
				prob = Random.Range (0, maxProb);
				Debug.Log ("Prob: " + prob);
				if ((prob > noEnemyProb) && (prob <= (noEnemyProb + stoppedEnemyProb))) {
					enemy = Instantiate (enemyPrefab) as GameObject;
					enemyManager = enemy.GetComponent<EnemyManager> ();
				} else if (prob >= (noEnemyProb + stoppedEnemyProb)) {
					enemy = Instantiate (enemyPrefab);
					enemyManager = enemy.GetComponent<EnemyManager> ();
					enemyManager.moving = true;
				}
				if (enemy != null) {
					lastEnemyPos = posIndex;
					side = GetSide ();
					spawnPoint = Instantiate (SpawnPointPrefab) as GameObject;
					spawnPoint.transform.position = new Vector3 (enemy.transform.position.x * side, module.transform.position.y, module.transform.position.z);
					enemy.transform.position = new Vector3 (enemy.transform.position.x * side, module.transform.position.y, module.transform.position.z);
					enemyManager.startSide = spawnPoint;
					enemy = null;
				}
			}
			if(posIndex == startLength) {
				yield return null;
			}
		}
	}

	private int GetSide() {
		int side;

		side = Random.Range (0, 2);
		Debug.Log("Side: " + side);
		return side > 0 ? 1 : -1;
	}
}
