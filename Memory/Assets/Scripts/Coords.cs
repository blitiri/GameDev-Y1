using System;

public class Coords
{
	private int col;
	private int row;

	public Coords () {
	}

	public Coords (int row, int col)
	{
		this.row = row;
		this.col = col;
	}

	public void SetRow(int row) {
		this.row = row;
	}

	public int GetRow() {
		return row;
	}

	public void SetCol(int col) {
		this.col = col;
	}

	public int GetCol() {
		return col;
	}

	public bool Equals(Coords other) {
		return (other != null) && (other.GetRow() == row) && (other.GetCol() == col);
	}
}
