using UnityEngine;
using System.Collections;

public class CatchedSymbol {
	private int level;
	private GameObject symbol;

	public CatchedSymbol(int level, GameObject symbol) {
		this.level = level;
		this.symbol = symbol;
	}

	public float GetLevel() {
		return level;
	}

	public GameObject GetSymbol() {
		return symbol;
	}
}

