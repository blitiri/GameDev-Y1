using UnityEngine;
using System.Collections;

public class WheelManager : MonoBehaviour {
	public float rotationSpeed = 1;

	void Awake() {
		MeshRenderer wheelPartRenderer;
		Color[] colors;
		int colorIndex;

		colors = GameManager.instance.colors;
		colorIndex = 0;
		foreach (Transform child in gameObject.transform) {
			wheelPartRenderer = child.gameObject.GetComponent<MeshRenderer> ();
			wheelPartRenderer.material.color = colors [colorIndex % colors.Length];
			colorIndex++;
		}
	}

	// Update is called once per frame
	void Update () {
		Rotate ();	
	}

	private void Rotate() {
		gameObject.transform.Rotate (Vector3.forward * rotationSpeed * Time.deltaTime);
	}
}
