using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	private int cardsRotatedCounter;
	private CardManager[] cardsRotated;

	void Awake() {
		instance = this;
		cardsRotatedCounter = 0;
		cardsRotated = new CardManager[2];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool CanRotate(CardManager cardManager) {
		bool rotate;

		rotate = false;
		if (!cardManager.IsFront ()) {
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
}
