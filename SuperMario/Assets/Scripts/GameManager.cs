using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject castle;
	public GameObject player;
	public GameObject levelStartPlace;
	public GameObject levelEndPlace;
	public GameObject camera;
	public UISlider positionTracker;
	private Renderer castleMeshRenderer;
	private int coins;
	private bool startLevel;
	private float levelLength;

	void Awake() {
		instance = this;
		StartLevel ();
		castleMeshRenderer = gameObject.GetComponent<Renderer> ();
	}

	// Use this for initialization
	void Start () {
		coins = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(startLevel) {
			StartLevel ();
		}
		UpdatePositionTracker ();
		float offset = Time.time;
		castleMeshRenderer.material.SetTextureOffset ("_NormalMap", new Vector2 (offset, 0));
	}

	void LateUpdate() {
		Vector3 cameraPosition;

		cameraPosition = camera.transform.position;
		camera.transform.position = new Vector3 (player.transform.position.x, cameraPosition.y, cameraPosition.z);
	}

	private void UpdatePositionTracker() {
		float positionFromStart;

		positionFromStart = (player.transform.position.x - levelStartPlace.transform.position.x);
		positionTracker.value = (positionFromStart < 0 ? 0 : positionFromStart) / levelLength;
	}

	private void StartLevel() {
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		player.transform.position = levelStartPlace.transform.position;
		startLevel = false;
		levelLength = levelEndPlace.transform.position.x - levelStartPlace.transform.position.x;
	}

	public void CatchedCoin(int coinValue) {
		coins += coinValue;
		Debug.Log ("Coins: " + coins);
	}

	public void PlayerDie() {
		startLevel = true;
	}

	public void PlayerWin() {
		Debug.Log ("Player win!!!");
	}
}
