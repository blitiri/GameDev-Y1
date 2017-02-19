using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject cardsParent;
	public GameObject cardPrefab;
	public string[] cardsNames;
	public int totRows;
	public int totCols;
	public int thoughtDeep;
	private const int idleTurn = 0;
	private const int playerTurn = 1;
	private const int aiTurn = 2;
	private int turn;
	private int cardsRotatedCounter;
	private CardManager[] cardsRotated;
	private GameObject[,] cards;
	private bool[,] selectableCards;
	private AI ai;
	private int turnComplitedCounter;

	void Awake() {
		instance = this;
		ai = new AI (thoughtDeep, totRows, totCols);
		cardsRotatedCounter = 0;
		cardsRotated = new CardManager[2];
		Generate ();
		turn = playerTurn;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (turn) {
		case playerTurn:
			break;
		case aiTurn:
			ai.ChooseMoves ();
			break;
		case idleTurn:
			break;
		default:
			break;
		}
	}

	public bool CanRotate(CardManager cardManager) {
		bool rotate;

		rotate = false;
		if (!cardManager.IsFront () && (turn != idleTurn)) {
			if (cardsRotatedCounter < 2) {
				cardsRotated [cardsRotatedCounter++] = cardManager;
				rotate = true;
			}
		}
		return rotate;
	}

	public void CheckCard() {
		if (cardsRotatedCounter == 2) {
			if (!cardsRotated [0].GetCardName ().Equals (cardsRotated [1].GetCardName ())) {
				cardsRotated [0].TurnCardToBack ();
				cardsRotated [1].TurnCardToBack ();
			} else {
				cardsRotated [0] = null;
				cardsRotated [1] = null;
			}
			cardsRotatedCounter = 0;
		}
	}

	public bool IsCardSelectable(Coords cardCoords) {
		return selectableCards[cardCoords.GetRow(), cardCoords.GetCol()];
	}

	public void 

	private void Generate() {
		int[] cardsCounter;
		int cardIndex;
		int row;
		int col;

		cardsCounter = new int[cardsNames.Length];
		for (cardIndex = 0; cardIndex < cardsCounter.Length; cardIndex++) {
			cardsCounter [cardIndex] = 0;
		}
		cards = new GameObject[totRows, totCols];
		selectableCards = new bool[totRows, totCols];
		for (row = 0; row < totRows; row++) {
			for (col = 0; col < totCols; col++) {
				selectableCards [row, col] = true;
				cards [row, col] = Instantiate (cardPrefab) as GameObject;
				cards [row, col].transform.SetParent (cardsParent.transform);
				do {
					cardIndex = Random.Range(0, cardsNames.Length);
				} while(cardsCounter [cardIndex] == 2);
				cardsCounter [cardIndex]++;
				cards [row, col].tag = cardsNames [cardIndex];
			}
		}
	}
}
