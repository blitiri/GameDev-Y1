using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenManager : MonoBehaviour {
	public UISprite canvas;
	public UISprite startButton;
	public UISprite quitButton;
	public Color startLerpColor = new Color (1, 1, 1, 0.1f);
	public Color endLerpColor = new Color (1, 1, 1, 1);
	public float colorLerpSpeed = 0.02f;
	private float lerpProgress;
	private float colorLerpIncr;

	void Awake() {
		colorLerpIncr = colorLerpSpeed;
	}

	// Use this for initialization
	void Start () {
		lerpProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(lerpProgress < 1) {
			startButton.color = Color.Lerp (startLerpColor, endLerpColor, lerpProgress);
			quitButton.color = Color.Lerp (startLerpColor, endLerpColor, lerpProgress);
			lerpProgress += colorLerpIncr;
		}
	}

	public void StartGame() {
		SceneManager.LoadScene ("Platform2D");
	}

	public void EndGame() {
		Debug.Log ("Fine gioco");
		Application.Quit ();
	}
}
