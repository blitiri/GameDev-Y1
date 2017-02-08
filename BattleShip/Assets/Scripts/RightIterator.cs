﻿using UnityEngine;
using System.Collections;

public class RightIterator : CoordsIterator {
	public override System.Collections.IEnumerator GetEnumerator()
	{
		Coords coords;
		int index;

		for(index = 0; index < shipSize; index++) {
			coords = new Coords (startRow, startCol + index);
			yield return coords;
		}
	}

	public RightIterator(int startRow, int startCol, int shipSize) {
		this.startRow = startRow;
		this.startCol = startCol;
		this.shipSize = shipSize;
	}

	public override CoordsIterator Reset() {
		return new RightIterator (startRow, startCol, shipSize);
	}
}
