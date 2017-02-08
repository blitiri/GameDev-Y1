using UnityEngine;
using System.Collections;

public class CheckPointManager : MonoBehaviour {
	public const int notSet = 0;
	public const int nothing = -1000;
	public const int fantastic = 2;
	public const int good = 1;
	public const int bad = 0;
	public const int missing = -10;
	[HideInInspector]
	public float fantasticDistance = .1f;
	[HideInInspector]
	public float goodDistance = .5f;
	[HideInInspector]
	public float badDistance = .7f;
	public string targetLayer;
	private const int ignoreRaycastLayer = 2;
	private int layerMask;

	void Awake() {
		gameObject.layer = ignoreRaycastLayer;
		if (targetLayer.Equals ("LeftLayer")) {
			layerMask = 1 << 8;
		} else if (targetLayer.Equals ("UpLayer")) {
			layerMask = 1 << 9;
		} else if (targetLayer.Equals ("DownLayer")) {
			layerMask = 1 << 10;
		} else if (targetLayer.Equals ("RightLayer")) {
			layerMask = 1 << 11;
		} else {
			layerMask = 0;
		}
	}

	public GameObject CheckForCatch() {
		GameObject symbol;
		SymbolManager symbolManager;
		Renderer checkPointRenderer;
		RaycastHit hit;
		Vector3 upRaycast;
		Vector3 downRaycast;
		float distance;
		float rayLength;
		int catchLevel;

		distance = float.MaxValue;
		symbol = null;
		checkPointRenderer = gameObject.GetComponent<MeshRenderer> ();
		rayLength = checkPointRenderer.bounds.size.y *  10;
		upRaycast = new Vector3 (0, rayLength, 0);
		downRaycast = new Vector3 (0, -rayLength, 0);
		Debug.Log("Layer mask: " + layerMask);
		Debug.DrawRay (transform.position, downRaycast, Color.magenta, 1);
		if (Physics.Raycast(transform.position, downRaycast, out hit, layerMask)) {
			distance = hit.distance;
			symbol = hit.transform.gameObject;
		}
		Debug.DrawRay (transform.position, upRaycast, Color.magenta, 1);
		if (Physics.Raycast(transform.position, upRaycast, out hit, layerMask)) {
			if (hit.distance < distance) {
				distance = hit.distance;
				symbol = hit.transform.gameObject;
			}
		}
		if (symbol) {
			if (Mathf.RoundToInt(distance) == Mathf.RoundToInt(float.MaxValue)) {
				catchLevel = nothing;
			}
			else if(distance < fantasticDistance) {
				catchLevel = fantastic;
			}
			else if(distance < goodDistance) {
				catchLevel = good;
			}
			else if(distance < badDistance) {
				catchLevel = bad;
			}
			else {
				catchLevel = missing;
			}
			symbolManager = symbol.GetComponent<SymbolManager> ();
			symbolManager.SetCatchLevel (catchLevel);
		}
		return symbol;
	}
}
