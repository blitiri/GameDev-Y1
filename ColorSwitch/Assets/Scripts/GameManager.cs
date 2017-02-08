using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject wheelPrefab;
	public GameObject colorSwitcherPrefab;
	public GameObject finishPrefab;
	public GameObject player;
	public GameObject mainCamera;
	public int pathLength = 3;
	public float objectDistance = 5;
	public float currentGenerationPos;
	[HideInInspector]
	public Color[] colors = {
		new Color (0.16f, 0.74f, 0.44f, 1),
		new Color (0.84f, 0.16f, 0, 1),
		new Color (0.36f, 0.14f, 0.33f, 1),
		new Color (0.09f, 0.15f, 0.63f, 1)
	};
	private MeshRenderer playerRenderer;
	private const int startingGenerations = 2;
	private int generationCounter;
	private GameObject[] createdObjects;
	private int createdObjectIndex;
	private Vector3 playerStartPosition;
	bool running;

	void Awake() {
		int generationIndex;

		running = true;
		instance = this;
		createdObjects = new GameObject[pathLength * 2];
		createdObjectIndex = 0;
		generationCounter = 0;
		playerStartPosition = player.transform.position;
		playerRenderer = player.GetComponent<MeshRenderer> ();
		SwitchColor();
		currentGenerationPos = 0;
		for (generationIndex = 0; generationIndex < startingGenerations; generationIndex++) {
			Generate ();
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			if (player.transform.position.y >= (currentGenerationPos - (startingGenerations * objectDistance))) {
				Generate ();
			}
		}
	}

	void LateUpdate() {
		if (running) {
			UpdateCameraPosition ();
		}
	}

	private void Generate() {
		GameObject wheel;
		GameObject colorSwitcher;
		GameObject finish;

		if (generationCounter < pathLength) {
			generationCounter++;
			currentGenerationPos += objectDistance;
			wheel = Instantiate (wheelPrefab);
			wheel.transform.position = new Vector3 (player.transform.position.x, currentGenerationPos, player.transform.position.z);
			createdObjects [createdObjectIndex++] = wheel;
			currentGenerationPos += objectDistance;
			if (generationCounter < pathLength) {
				colorSwitcher = Instantiate (colorSwitcherPrefab);
				colorSwitcher.transform.position = new Vector3 (player.transform.position.x, currentGenerationPos, player.transform.position.z);
				createdObjects [createdObjectIndex++] = colorSwitcher;
			} else {
				finish = Instantiate (finishPrefab);
				finish.transform.position = new Vector3 (player.transform.position.x, currentGenerationPos, player.transform.position.z);
				createdObjects [createdObjectIndex++] = finish;
			}
		}
	}

	public void WheelCorssing(Color colorCrossed) {
		if(playerRenderer.material.color != colorCrossed) {
			Destroy (player);
			running = false;
		}
	}

	public void SwitchColor() {
		playerRenderer.material.color = colors [Random.Range (0, colors.Length)];
	}

	public void Finish() {
		Rigidbody playerRigidbody;

		running = false;
		playerRigidbody = player.GetComponent <Rigidbody> ();
		playerRigidbody.isKinematic = true;
		foreach(GameObject createdObject in createdObjects) {
			Destroy(createdObject);
		}
		player.transform.position = playerStartPosition;
		UpdateCameraPosition ();
	}

	public bool isRunning() {
		return running;
	}

	private void UpdateCameraPosition() {
		Vector3 cameraPosition;
		Vector3 playerPosition;

		cameraPosition = mainCamera.transform.position;
		playerPosition = player.transform.position;
		mainCamera.transform.position = new Vector3 (cameraPosition.x, playerPosition.y, cameraPosition.z);
	}
}
