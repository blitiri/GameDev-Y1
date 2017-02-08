using UnityEngine;
using System.Collections;
using System;

public class InitGame : MonoBehaviour {
	public GameObject land;
	public int enemyQuantity = 5;
	public int gunQuantity = 2;
	public int tommyGunQuantity = 2;
	public GameObject enemyPrefab;
	public GameObject playerPreab;
	public GameObject gunPrefab;
	public GameObject tommyGunPreab;
	private GameObject[] istantiatedObjects;

	void Awake() {
		Vector3 position;
		int objectIndex;
		int index;

		Renderer landRenderer = land.GetComponent<Renderer> ();
		istantiatedObjects = new GameObject[enemyQuantity + gunQuantity + tommyGunQuantity];
		objectIndex = 0;
		for(index = 0; index < enemyQuantity; index++, objectIndex++) {
			do {
				position = landRenderer.bounds.size * UnityEngine.Random.value;
				istantiatedObjects [objectIndex] = Instantiate (enemyPrefab, position, Quaternion.identity) as GameObject;
			} while(!isValidPosition (istantiatedObjects [objectIndex]));
		}
		for(index = 0; index < gunQuantity; index++, objectIndex++) {
			do {
				position = landRenderer.bounds.size * UnityEngine.Random.value;
				istantiatedObjects[objectIndex] = Instantiate (gunPrefab, position, Quaternion.identity) as GameObject;
			} while(!isValidPosition (istantiatedObjects [objectIndex]));
		}
		for(index = 0; index < tommyGunQuantity; index++, objectIndex++) {
			do {
				position = landRenderer.bounds.size * UnityEngine.Random.value;
				istantiatedObjects[objectIndex] = Instantiate (tommyGunPreab, position, Quaternion.identity) as GameObject;
			} while(!isValidPosition (istantiatedObjects [objectIndex]));
		}
		do {
			position = landRenderer.bounds.size * UnityEngine.Random.value;
			istantiatedObjects[objectIndex] = Instantiate (playerPreab, position, Quaternion.identity) as GameObject;
		} while(!isValidPosition (istantiatedObjects [objectIndex]));
	}

	private Boolean isValidPosition(GameObject instance) {
		return true;
	}
}
