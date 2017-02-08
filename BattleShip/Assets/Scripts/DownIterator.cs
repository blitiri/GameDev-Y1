using UnityEngine;
using System.Collections;

public class DownIterator : CoordsIterator {
	public override System.Collections.IEnumerator GetEnumerator()
	{
		Coords coords;
		int index;

		for(index = 0; index < shipSize; index++) {
			coords = new Coords (startRow + index, startCol);
			yield return coords;
		}
	}

	public DownIterator(int startRow, int startCol, int shipSize) {
		this.startRow = startRow;
		this.startCol = startCol;
		this.shipSize = shipSize;
	}

	public override CoordsIterator Reset() {
		return new DownIterator (startRow, startCol, shipSize);
	}
}
