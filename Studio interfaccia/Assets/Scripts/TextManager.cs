using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateText (score);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) {
			score += 10;
			UpdateText (score);
		}
	}

	private void UpdateText(int score) {
		Text scoreText;

		scoreText = gameObject.GetComponent<Text> ();
		scoreText.text = "Score: " + score;
	}
}
