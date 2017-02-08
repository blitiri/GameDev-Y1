using UnityEngine;
using System.Collections;

public class DestroyableWallManager : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
		if ((GameManager.instance != null) && (player != null)) {
			GameManager.instance.AddPoints (100, player);
		}
    }
}
