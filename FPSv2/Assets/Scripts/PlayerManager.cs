using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	// Max stroke load (in seconds)
	public float maxStrokeLoad = 5;
	public float movementSpeed = 25;
	public float rotationSpeed = 30;
	private float loadingStartTime;
	public GameObject barrel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	private void Move() {
		float strokeLoad;

		if (Input.GetKey (KeyCode.W)) {
			gameObject.transform.Translate (gameObject.transform.forward * Time.deltaTime * movementSpeed, Space.World);
		} else if (Input.GetKey (KeyCode.S)) {
			gameObject.transform.Translate (gameObject.transform.forward * Time.deltaTime * -movementSpeed, Space.World);
		}
		if (Input.GetKey (KeyCode.A)) {
			gameObject.transform.Rotate (gameObject.transform.up * Time.deltaTime * -rotationSpeed, Space.World);
		} else if (Input.GetKey (KeyCode.D)) {
			gameObject.transform.Rotate (gameObject.transform.up * Time.deltaTime * rotationSpeed, Space.World);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			loadingStartTime = Time.time;
			Debug.Log ("Caricamento iniziato");
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			strokeLoad = Time.time - loadingStartTime;
			if (strokeLoad > maxStrokeLoad) {
				strokeLoad = maxStrokeLoad;
			}
			Debug.Log ("Caricamento finito: " + strokeLoad);
			GameManager.instance.Fire (barrel.transform, strokeLoad / maxStrokeLoad);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag.Equals ("Bullet")) {
			GameManager.instance.PlayerKilled ();
		}
	}
}
