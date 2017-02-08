using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {
	public Player player1;
	public Player player2;
	public Player player3;
	public Player player4;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ManageInput ();
	}

	private void ManageInput () {
		if (Input.GetKeyDown (KeyCode.A)) {
			player1.Move (Vector3.up);
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			player2.Move (Vector3.up);
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			player3.Move (Vector3.up);
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			player4.Move (Vector3.up);
		}
	}
}
