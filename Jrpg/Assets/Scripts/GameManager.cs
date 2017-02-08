using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject[] players;
	public GameObject[] enemies;
	private const int noAttack = 0;
	private const int rangeAttack = 1;
	private const int physicAttack = 2;
	private bool playerRound;
	private bool running;
	private GameObject currentPlayer;
	private int selectedAttackType;
	private GameObject selectedOpponent;
	private GameObject selectedStriker;

	// Use this for initialization
	void Start () {
		playerRound = true;
		running = true;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray;

		if(running) {
			if (playerRound) {
				if (selectedAttackType != noAttack) {
					if (Input.GetMouseButtonDown (0)) {
						ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						if (Physics.Raycast (ray, out hit)) {
							if (hit.transform.gameObject.tag == "Player") {
								selectedStriker = hit.transform.gameObject;
								selectedOpponent = enemies [Random.Range (0, enemies.Length)];
								Highlight (selectedStriker, players, Color.green);
								Highlight (selectedOpponent, enemies, Color.red);
							}
						}
					}
				}
				if(Input.GetKey(KeyCode.A)) {
					selectedAttackType = rangeAttack;
				}
				else if(Input.GetKey(KeyCode.S)) {
					selectedAttackType = physicAttack;
				}
			}
			else {
				selectedStriker = enemies[Random.Range (0, enemies.Length)];
				selectedOpponent = players [Random.Range (0, players.Length)];
				selectedAttackType = rangeAttack;
				Highlight (selectedStriker, enemies, Color.green);
				Highlight (selectedOpponent, players, Color.red);
			}
			ExecuteAttack ();
		}
	}

	private void ExecuteAttack() {
		PlayerManager opponentManager;
		PlayerManager strikerManager;

		if((selectedStriker != null) && (selectedOpponent != null) && (selectedAttackType != noAttack)) {
			Debug.Log ("Attacco del player: " + playerRound);
			opponentManager = selectedOpponent.GetComponent<PlayerManager>();
			strikerManager = selectedStriker.GetComponent<PlayerManager>();
			switch(selectedAttackType) {
			case rangeAttack:
				opponentManager.lifePoint -= strikerManager.demagePoint;
				break;
			case physicAttack:
				opponentManager.lifePoint -= strikerManager.demagePoint;
				break;
			default:
				break;
			}
			if(opponentManager.lifePoint <= 0) {
				Destroy (selectedOpponent);
			}
			selectedStriker = null;
			selectedOpponent = null;
			selectedAttackType = noAttack;
			playerRound = !playerRound;
		}
	}

	private void Highlight(GameObject go, GameObject[] others, Color color) {
		MeshRenderer objectRenderer;
		int index;

		for(index = 0; index < others.Length; index++) {
			SetColor (others [index], Color.white);
		}
		SetColor(go, color);
	}

	private void SetColor(GameObject go, Color color) {
		MeshRenderer objectRenderer;

		objectRenderer = go.GetComponent<MeshRenderer> ();
		objectRenderer.material.color = color;
	}
}
