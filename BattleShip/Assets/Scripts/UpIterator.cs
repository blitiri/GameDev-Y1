using UnityEngine;
using System.Collections;

public class UpIterator : CoordsIterator {
	public override System.Collections.IEnumerator GetEnumerator()
	{
		Coords coords;
		int index;

		for(index = 0; index < shipSize; index++) {
			coords = new Coords (startRow - index, startCol);
			yield return coords;
		}
	}

	public UpIterator(int startRow, int startCol, int shipSize) {
		this.startRow = startRow;
		this.startCol = startCol;
		this.shipSize = shipSize;
	}

	public override CoordsIterator Reset() {
		return new UpIterator (startRow, startCol, shipSize);
	}
}
