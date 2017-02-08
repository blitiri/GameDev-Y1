using UnityEngine;
using System.Collections;

public class Ship {
	private ShipPart[] shipParts;
	private bool[] hits;

	public bool isMyCoord(Coords coords) {
		int partIndex;
		bool myCoord;

		for (partIndex = 0, myCoord = false; partIndex < shipParts.Length; partIndex++) {
			myCoord |= shipParts [partIndex].isMyCoord (coords);
		}
		return myCoord;
	}

	public bool isHit(Coords coords, Color hitColor) {
		int partIndex;
		bool hit;

		for (partIndex = 0, hit = false; partIndex < shipParts.Length; partIndex++) {
			if (shipParts [partIndex].isHit (coords, hitColor)) {
				hit = true;
				hits [partIndex] = true;
			}
		}
		return hit;
	}

	public bool isSunk(Color sunkColor) {
		bool sunk;
		int partIndex;

		for (partIndex = 0, sunk = true; partIndex < hits.Length; partIndex++) {
			sunk &= hits [partIndex];
		}
		if (sunk) {
			for (partIndex = 0; partIndex < shipParts.Length; partIndex++) {
				shipParts [partIndex].sunk (sunkColor);
			}
		}
		return sunk;
	}

	public void setShipParts(ShipPart[] shipParts) {
		this.shipParts = shipParts;
		hits = new bool[shipParts.Length];
	}
}
