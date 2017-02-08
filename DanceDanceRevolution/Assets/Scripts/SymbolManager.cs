using UnityEngine;
using System.Collections;

public class SymbolManager : MonoBehaviour {
	public Vector3 fromPosition;
	public Vector3 toPosition;
	public float speed;
	private int catchLevel;
	private float lerpPerc;

	// Use this for initialization
	void Start () {
		catchLevel = CheckPointManager.notSet;
		lerpPerc = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	public int GetCatchLevel() {
		return catchLevel;
	}

	public void SetCatchLevel(int catchLevel) {
		this.catchLevel = catchLevel;
	}

	private void Move() {
		Vector3 newPosition;

		lerpPerc += (speed * Time.deltaTime);
		if (lerpPerc > 1) {
			if (catchLevel == CheckPointManager.notSet) {
				GameManager.instance.MissingCatch ();
			}
			Destroy (gameObject);
		} else {
			newPosition = Vector3.Lerp (fromPosition, toPosition, lerpPerc);
			gameObject.transform.position = newPosition;
		}
	}
}
