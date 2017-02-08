using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject player;
	public GameObject ghostPrefab;
	public GameObject coinPrefab;
	public Transform startPosition;
	public GameObject[] spawnPoints;
	public Rigidbody2D plyerRigidbody;
	public UILabel ghostsPicked;
	public UILabel coinsPicked;
	public UILabel time;
	public int totalTime = 10;
	public float backToStartSpeed = .5f;
	private float backToStartLerpStep;
	private int coins;
	private int ghosts;
	private int totalGhosts;
	private float startTime;
	private bool goToStartPosition;
	private float toStartPositionLerp;
	private Vector3 currentPlayerPosition;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		player.transform.position = startPosition.position;
		plyerRigidbody.isKinematic = false;
		goToStartPosition = false;
		toStartPositionLerp = -1;
		coins = 0;
		ghosts = 0;
		totalGhosts = 0;
		startTime = Time.time;
		time.text = "" + totalTime;
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (goToStartPosition) {
			Debug.Log ("Start lerp to start position");
			toStartPositionLerp = 0;
			currentPlayerPosition = player.transform.position;
			backToStartLerpStep = 1 / (Vector3.Distance (currentPlayerPosition, startPosition.position) / backToStartSpeed);
			goToStartPosition = false;
		}
		if (toStartPositionLerp >= 0) {
			Debug.Log ("Lerping to start position (" + toStartPositionLerp + ")");
			toStartPositionLerp += backToStartLerpStep;
			player.transform.position = Vector3.Lerp (currentPlayerPosition, startPosition.position, toStartPositionLerp);
			if (toStartPositionLerp >= 1) {
				Debug.Log ("Lerping to start position ended");
				toStartPositionLerp = -1;
				player.SetActive (true);
			}
		} else {
			CheckPlayerFallen ();
			CheckPlayerWin ();
		}
		UpdateTime ();
	}

	private void UpdateTime() {
		float remainingTime;

		remainingTime = totalTime + startTime - Time.time;
		if (remainingTime < 0) {
			remainingTime = 0;
		}
		time.text = "" + (int)remainingTime;
		if (remainingTime <= 0) {
			Debug.Log ("You loose!!!!");
			SceneManager.LoadScene ("DeadScreen");
		}
	}

	private void CheckPlayerWin() {
		if(ghosts == totalGhosts) {
			Debug.Log ("You win!!!");
			SceneManager.LoadScene ("WinScreen");
		}
	}

	private void Spawn() {
		int spawnIndex;

		for (spawnIndex = 0; spawnIndex < spawnPoints.Length; spawnIndex++) {
			switch(Random.Range(0, 3)) {
			case 0:
				break;
			case 1:
				Instantiate (ghostPrefab, spawnPoints [spawnIndex].transform.position, Quaternion.identity);
				totalGhosts++;
				break;
			case 2:
				Instantiate (coinPrefab, spawnPoints [spawnIndex].transform.position, Quaternion.identity);
				break;
			}
		}
	}

	private void CheckPlayerFallen() {
		if(player.transform.position.y < -5) {
			plyerRigidbody.isKinematic = true;
			SceneManager.LoadScene ("DeadScreen");
		}
	}

	public void GhostTouched() {
		Debug.Log ("You have touch a ghost!!!!");
		goToStartPosition = true;
		player.SetActive (false);
	}

	public void CoinPicked(GameObject coin) {
		Destroy (coin);
		coins++;
		coinsPicked.text = "" + coins;
		Debug.Log ("Coins: " + coins);
	}

	public void GhostPicked(GameObject ghost) {
		Destroy (ghost);
		ghosts++;
		ghostsPicked.text = "" + ghosts;
		Debug.Log ("Ghosts: " + ghosts);
	}
}
