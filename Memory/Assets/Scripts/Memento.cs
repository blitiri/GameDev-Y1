using UnityEngine;
using System.Collections;

public class Memento {
	private Coords coords;
	private string cardName;

	public void SetCoords(Coords coords) {
		this.coords = coords;
	}

	public Coords GetCoords() {
		return coords;
	}

	public void SetCardName(string cardName) {
		this.cardName = cardName;
	}

	public string GetCardName() {
		return cardName;
	}
}
