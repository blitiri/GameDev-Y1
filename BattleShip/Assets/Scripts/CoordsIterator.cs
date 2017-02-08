using UnityEngine;
using System.Collections;

public abstract class CoordsIterator : IEnumerable {
	public const int up = 0;
	public const int down = 1;
	public const int right = 2;
	public const int left = 3;
	protected int startRow;
	protected int startCol;
	protected int shipSize;

	public static CoordsIterator getIterator(int direction, int startRow, int startCol, int shipSize, int battleFieldWidth, int battleFieldHeight) {
		CoordsIterator iterator;

		validateCoord (startRow, battleFieldHeight, "row");
		validateCoord (startCol, battleFieldWidth, "col");
		switch (direction) {
		case up:
			if ((startRow - shipSize + 1) < 0) {
				throw new System.Exception ("Invalid start row");
			}
			iterator = new UpIterator (startRow, startCol, shipSize);
			break;
		case down:
			if((startRow + shipSize) > battleFieldHeight) {
				throw new System.Exception ("Invalid start row");
			}
			iterator = new DownIterator (startRow, startCol, shipSize);
			break;
		case right:
			if((startCol + shipSize) > battleFieldWidth) {
				throw new System.Exception ("Invalid start col");
			}
			iterator = new RightIterator (startRow, startCol, shipSize);
			break;
		case left:
			if((startCol - shipSize + 1) < 0) {
				throw new System.Exception ("Invalid start col");
			}
			iterator = new LeftIterator (startRow, startCol, shipSize);
			break;
		default:
			iterator = null;
			break;
		}
		return iterator;
	}

	public abstract System.Collections.IEnumerator GetEnumerator ();

	public abstract CoordsIterator Reset();

	private static void validateCoord(int coord, int size, string coordName) {
		if((coord < 0) || (coord >= size)) {
			throw new System.Exception ("Invalid start " + coordName);
		}
	}
}
