using UnityEngine;
using System.Collections;

public class EggsGenerator : MonoBehaviour {
	public float frequency = 1;
	public GameObject eggPrefab;
	public GameObject spawnPointPrefab;
	public float timer = 0;
	public int spawnPointsNumber = 3;
	public int changeLevelFrequency = 3;
	public int difficultFactor = 1;
	private GameObject[] spawnPoints = null;
	[SerializeField]
	private int level = 1;
	private int lastChangeLevelTime;

	void Awake() {
		Vector3 position;
		int index;
		float distance;

		spawnPoints = new GameObject[spawnPointsNumber];
		position = new Vector3 (0, 10, 0);
		distance = 10.0f / (spawnPointsNumber + 1);
		for(index = 0; index < spawnPointsNumber; index++) {
			position.x = distance * (index + 1) - 5;
			spawnPoints [index] = Instantiate (spawnPointPrefab);
			spawnPoints [index].transform.Translate (position, Space.World);
		}
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isPlayerAlive ()) {
			Generate ();
			IncreaseDifficulty ();
		}
	}

	private void Generate() {
		GameObject spawnPoint = null;
		GameObject egg = null;

		if (timer > frequency) {
			spawnPoint = GetRandomSpawnPoint ();
			egg = Instantiate (eggPrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject;
			egg.GetComponent<Rigidbody> ().AddForce (-Vector3.up * 10);
			timer = 0;
		} else {
			timer += Time.deltaTime;
		}
	}

	private GameObject GetRandomSpawnPoint() {
		int spawnPointsIndex;

		spawnPointsIndex = (int)(Random.Range(0, spawnPoints.Length) + .5f);
		return spawnPoints [spawnPointsIndex];
	}

	private void IncreaseDifficulty () {
		int currentTime;

		currentTime = (int)Time.time;
		if((currentTime > 0) && (currentTime > lastChangeLevelTime) && (currentTime % changeLevelFrequency == 0)) {
			level++;
			frequency = frequency - (1.0f / (level * 2 * difficultFactor));
			lastChangeLevelTime = currentTime;
		}
	}
}
