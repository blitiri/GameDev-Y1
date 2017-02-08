using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject player;
	public int score = 0;
	public int lifes = 3;

	void Awake() {
		instance = this;
	}

	public void KillPlayer() {
		lifes--;
		if(lifes == 0) {
			Destroy (player);
		}
	}

	public void AddScore(int eggScore) {
		score += eggScore;
	}

	public bool isPlayerAlive() {
		return lifes > 0;
	}
}
