using UnityEngine;
using System.Collections;

public class CheckButtonManager : MonoBehaviour {
	private const int leftButton = 0;
	private MeshRenderer buttonRenderer;
	bool clickedMe;

	void Awake() {
		buttonRenderer = gameObject.GetComponent<MeshRenderer> ();
		buttonRenderer.enabled = false;
		clickedMe = false;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (leftButton)) {
			if (clickedMe) {
				GameManager.instance.CheckAttempt ();
				clickedMe = false;
			} else if (!buttonRenderer.enabled && GameManager.instance.IsAttemptComplete ()) {
				buttonRenderer.enabled = true;
			}
		}
	}

	void OnMouseDown() {
		clickedMe = true;
	}
}
