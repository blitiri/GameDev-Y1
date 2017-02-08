using UnityEngine;
using System.Collections;

public class ShipPart {
	private GameObject part;
	private Coords coords;

	public bool isMyCoord(Coords coords) {
		return (this.coords.Equals(coords));
	}

	public bool isHit(Coords coords, Color hitColor) {
		Renderer partRenderer;
		bool hit;

		hit = isMyCoord(coords);
		if (hit) {
			partRenderer = part.GetComponent<MeshRenderer> ();
			partRenderer.material.color = hitColor;
		}
		return hit;
	}

	public void sunk(Color sunkColor) {
		Renderer partRenderer;

		partRenderer = part.GetComponent<MeshRenderer> ();
		partRenderer.material.color = sunkColor;
	}

	public void setCoords(Coords coords) {
		this.coords = coords;
	}

	public void setPart(GameObject part) {
		this.part = part;
	}
}
