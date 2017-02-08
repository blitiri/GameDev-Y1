using UnityEngine;
using System.Collections;

public class Coords {
	private int row;
	private int col;

	public Coords() {
	}

	public Coords(int row, int col) {
		this.row = row;
		this.col = col;
	}

	public void setRow(int row) {
		this.row = row;
	}

	public int getRow() {
		return row;
	}

	public void setCol(int col) {
		this.col = col;
	}

	public int getCol() {
		return col;
	}

	public bool Equals(Coords coords) {
		return (coords != null) && (row == coords.row) && (col == coords.col);
	}

	public override string ToString() {
		return "(" + row + ",  " + col + ")";
	}
}
