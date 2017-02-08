using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public UISlider slider;
	public float sliderIncrease = 1f;
	public float sliderDecrease = .1f;

	// Use this for initialization
	void Start () {
		slider.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (slider.value < 1) {
			slider.value -= sliderDecrease * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Space)) {
			slider.value += sliderIncrease * Time.deltaTime;
		}
	}

	public void Increase() {
	}
}
