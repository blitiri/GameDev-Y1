using UnityEngine;
using System.Collections;

public class FieldGenerator : MonoBehaviour {
	public int width = 21;
	public int height = 21;
	public int destroyableWallsFrequency = 30;
	public GameObject destroyableWallPrefab;
	public GameObject undestroyableWallPrefab;
	public GameObject fieldSpawnPoint;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	private char[,] field;
	private const char destroyableWall = 'D';
	private const char undestroyableWall = 'U';
	private const char forbiddenCell = 'F';

	// Use this for initialization
	void Start () {
		Generate ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void Generate() {
		field = new char[height, width];
		MarkForbiddenCells ();
		GenerateUndestroyableWalls ();
		GenerateDestroyableWalls ();
		GenerateField ();
	}

	private void MarkForbiddenCells() {
		// Top left corner
		field [1, 1] = forbiddenCell;
		field [2, 1] = forbiddenCell;
		field [1, 2] = forbiddenCell;
		// Top right corner
		field [1, width - 2] = forbiddenCell;
		field [2, width - 2] = forbiddenCell;
		field [1, width - 3] = forbiddenCell;
		// Bottom left corner
		field [height - 2, 1] = forbiddenCell;
		field [height - 3, 1] = forbiddenCell;
		field [height - 2, 2] = forbiddenCell;
		// Bottom right corner
		field [height - 2, width - 2] = forbiddenCell;
		field [height - 3, width - 2] = forbiddenCell;
		field [height - 2, width - 3] = forbiddenCell;
	}

	private void GenerateUndestroyableWalls () {
		int row;
		int col;

		for(row = 0; row < height; row++) {
			// First and last row
			if((row == 0) || (row == (height - 1))) {
				for(col = 0; col < width; col++) {
					if (isAllowedCell (row, col)) {
						field [row, col] = undestroyableWall;
					}
				}
			}
			// Even rows have walls in even columns
			else if(row % 2 == 0){
				for (col = 0; col < width; col += 2) {
					if (isAllowedCell (row, col)) {
						field [row, col] = undestroyableWall;
					}
				}
				// Required if width is even because (even - 1) % 2 == 1
				field [row, width - 1] = undestroyableWall;
			}
			// Odd rows are empty except first and last cell (frame cell)
			else {
				if (isAllowedCell (row, 0)) {
					field [row, 0] = undestroyableWall;
				}
				if (isAllowedCell (row, width - 1)) {
					field [row, width - 1] = undestroyableWall;
				}
			}
		}
	}

	private bool isAllowedCell(int row, int col) {
		return field [row, col] != forbiddenCell;
	}

	private void GenerateDestroyableWalls() {
		int row;
		int col;

		for(row = 0; row < height; row++) {
			for(col = 0; col < width; col++) {
				if((field[row, col] == '\0') && isAllowedCell(row, col) && IsToBeGenerated()) {
					field [row, col] = destroyableWall;
				}
			}
		}
	}

	private bool IsToBeGenerated() {
		return Random.Range (0, 100) < destroyableWallsFrequency;
	}

	private void GenerateField() {
		int row;
		int col;

		for (row = 0; row < height; row++) {
			for (col = 0; col < width; col++) {
				switch(field[row, col]) {
				case destroyableWall:
					GenerateWall (row, col, destroyableWallPrefab);
					break;
				case undestroyableWall:
					GenerateWall (row, col, undestroyableWallPrefab);
					break;
				default:
					break;
				}
			}
		}
		setPosition (player1, 1, 1);
		setPosition (player2, width - 2, height - 2);
		setPosition (player3, width - 2, 1);
		setPosition (player4, 1, height - 2);
	}

	private GameObject GenerateWall(int row, int col, GameObject wallPrefab) {
		return setPosition (Instantiate (wallPrefab) as GameObject, row, col);
	}

	private GameObject setPosition(GameObject obj, int row, int col) {
		if (obj != null) {
			obj.transform.parent = fieldSpawnPoint.transform;
			obj.transform.localPosition = new Vector3 (col, 0.5f, row);
		}
		return obj;
	}
}
