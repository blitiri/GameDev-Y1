using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject player;
	public Transform startPosition;
	public GameObject enemyPrefab;
	public Transform[] enemiesStartPositions;
	public Transform[] enemiesEndPositions;
	public int currentLevel = 1;
	private float startTime;
	private bool running;

	void Awake() {
		instance = this;
		running = false;
	}

	// Use this for initialization
	void Start () {
		GameObject enemy;
		EnemyManager enemyManager;
		int enemyIndex;

		running = true;
		startTime = Time.time;
		player.transform.position = startPosition.position;
		for(enemyIndex = 0; enemyIndex < enemiesStartPositions.Length; enemyIndex++) {
			enemy = Instantiate (enemyPrefab) as GameObject;
			enemyManager = enemy.GetComponent<EnemyManager> ();
			enemyManager.Init (player, enemiesStartPositions [enemyIndex], enemiesEndPositions [enemyIndex]);
			Debug.Log ("Enemy " + enemyIndex + " instantiated - start: " + enemiesStartPositions [enemyIndex] + ", end " + enemiesEndPositions [enemyIndex]);
		}
		running = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsRunning() {
		return running;
	}

	public void LevelCompleted() {
		Debug.Log ("Level completed in " + (int)(Time.time - startTime) + " seconds");
		running = false;
	}

	public void PlayerDied() {
		Debug.Log ("Looser!!!");
		running = false;
	}
}
