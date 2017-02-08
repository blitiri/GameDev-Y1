using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	public GameObject checkPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject Generate(GameObject symbolPrefab, float speed) {
		GameObject symbol;

		symbol = Instantiate (symbolPrefab) as GameObject;
		return symbol;
	}
}
