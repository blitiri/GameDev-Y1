using UnityEngine;
using System.Collections;

public class AI {
	private Memento[] mementoArray;
	private Coords firstMove;
	private Coords secondMove;
	private int totRows;
	private int totCols;
	private int currMemento;

	public AI(int thoughtDeep, int totRows, int totCols) {
		mementoArray = new Memento[thoughtDeep];
		this.totRows = totRows;
		this.totCols = totCols;
		currMemento = 0;
	}

	public void AddMemento(int row, int col, string cardName) {
		Memento memento;
		Coords coords;

		if (mementoArray.Length > 0) {
			coords = new Coords ();
			coords.SetRow (row);
			coords.SetCol (col);
			memento = new Memento ();
			memento.SetCardName (cardName);
			memento.SetCoords (coords);
			mementoArray [currMemento] = memento;
			currMemento = (currMemento + 1) % mementoArray.Length;
		}
	}

	public void RemoveMemento(Coords mementoCoords) {
		int mementoIndex;

		for (mementoIndex = 0; mementoIndex < mementoArray.Length; mementoIndex++) {
			if (mementoArray [mementoIndex].GetCoords ().Equals (mementoCoords)) {
				mementoArray [mementoIndex] = null;
			}
		}
	}

	public void ChooseMoves() {
		Memento firstMemento;
		Memento secondMemento;
		int firstMoveIndex;
		int secondMoveIndex;
		bool foundPair;

		foundPair = false;
		if (mementoArray.Length > 0) {
			for (firstMoveIndex = 0; !foundPair && (firstMoveIndex < mementoArray.Length - 1); firstMoveIndex++) {
				firstMemento = mementoArray [firstMoveIndex];
				if (firstMemento != null) {
					for (secondMoveIndex = firstMoveIndex + 1; !foundPair && (secondMoveIndex < mementoArray.Length); secondMoveIndex++) {
						secondMemento = mementoArray [secondMoveIndex];
						if (secondMemento != null) {
							if (firstMemento.GetCardName ().Equals (secondMemento.GetCardName ())) {
								firstMove = mementoArray [firstMoveIndex].GetCoords ();
								secondMove = mementoArray [secondMoveIndex].GetCoords ();
								foundPair = true;
							}
						}
					}
				}
			}
		} 
		if (!foundPair) {
			firstMove = ChooseMoveRandom (null);
			secondMove = ChooseMoveRandom (firstMove);
		}
	}

	public Coords GetFirstMove() {
		return firstMove;
	}

	public Coords GetSecondMove() {
		return secondMove;
	}

	private Coords ChooseMoveRandom(Coords alreadySelectedCoords) {
		Coords selectedCoords;

		selectedCoords = new Coords ();
		do {
			selectedCoords.SetRow(Random.Range (0, totRows));
			selectedCoords.SetCol(Random.Range (0, totCols));
		} while(GameManager.instance.IsCardSelectable(selectedCoords) || selectedCoords.Equals(alreadySelectedCoords) || IsMemento (selectedCoords));
		return selectedCoords;
	}

	private bool IsMemento(Coords coordsToCheck) {
		Coords coords;
		int mementoIndex;
		bool found;

		for (mementoIndex = 0, found = false; !found && (mementoIndex < mementoArray.Length); mementoIndex++) {
			coords = mementoArray [mementoIndex].GetCoords ();
			found = coords.Equals (coordsToCheck);
		}
		return found;
	}
}
