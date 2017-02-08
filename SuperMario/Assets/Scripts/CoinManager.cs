using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
	public float rotationSpeed = 200;
	public int coinValue = 10;

	// Update is called once per frame
	void Update () {
		Rotate ();
	}

	private void Rotate() {
		gameObject.transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		GameManager.instance.CatchedCoin (coinValue);
		Destroy (gameObject);
	}
}
