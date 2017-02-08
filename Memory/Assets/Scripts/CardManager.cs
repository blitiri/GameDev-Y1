using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {
	public UIButton uiButton;
	public UISprite uiSprite;
	public TweenRotation rotation;
	bool turnToFront;
	bool rotationCompleted;
	bool front;

	// Use this for initialization
	void Start () {
		turnToFront = true;
		rotationCompleted = false;
		front = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetCardName() {
		return gameObject.tag;
	}

	public bool IsFront() {
		return front;
	}

	public void TurnCardToFront() {
		if (GameManager.instance.CanRotate (this)) {
			Debug.Log ("TurnCardToFront " + GetCardName());
			TurnCard ();
		}
	}

	public void TurnCardToBack() {
		Debug.Log ("TurnCardToBack " + GetCardName());
		TurnCard ();
	}

	private void TurnCard() {
		rotationCompleted = false;
		rotation.enabled = true;
		Debug.Log ("1 rotationCompleted for card " + GetCardName() + ": " + rotationCompleted);
		rotation.PlayForward ();
		Debug.Log ("2 rotationCompleted for card " + GetCardName() + ": " + rotationCompleted);
	}

	public void CompleteCardTurning() {
		string spriteName;

		Debug.Log ("3 rotationCompleted for card " + GetCardName() + ": " + rotationCompleted);
		if (!rotationCompleted) {
			Debug.Log ("turnToFront: " + turnToFront);
			spriteName = (turnToFront ? gameObject.tag : "CardBack");
			uiSprite.spriteName = spriteName;
			uiButton.normalSprite = spriteName;
			Debug.Log ("CompleteCardTuring " + GetCardName() + " with " + spriteName);
			rotation.PlayReverse ();
			front = turnToFront;
			turnToFront = !turnToFront;
			rotationCompleted = true;
		}
		else {
			GameManager.instance.CheckCard ();
		}
	}
}
