using UnityEngine;
using System.Collections;

public class PegManager : MonoBehaviour {
	private const int leftButton = 0;
	private int clickCounter;
	private MeshRenderer pegRenderer;

	void Awake() {
		pegRenderer = gameObject.GetComponent<MeshRenderer> ();
	}

	// Use this for initialization
	void Start () {
		clickCounter = 0;
	}
	
	void OnMouseDown() {
		Color nextColor;
		int attemptIndex;

		if(!gameObject.tag.Equals("code")) {
			attemptIndex = int.Parse (gameObject.tag);
			nextColor = GameManager.instance.GetAttemptColor (attemptIndex, clickCounter++);
			pegRenderer.material.color = nextColor;
		}
	}
}
