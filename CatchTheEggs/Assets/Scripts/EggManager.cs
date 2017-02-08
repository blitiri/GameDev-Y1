using UnityEngine;
using System.Collections;

public class EggManager : MonoBehaviour {
	public int eggScore = 10;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag.Equals("Land")) {
			GameManager.instance.KillPlayer ();
		}
		else if(other.gameObject.tag.Equals("Player")) {
			GameManager.instance.AddScore (eggScore);
		}
		Destroy (gameObject);
	}
}
