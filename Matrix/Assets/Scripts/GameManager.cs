using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject cubePrefab;
	public GameObject parent;
	public int height = 10;
	public int width = 10;
	public float cubeDistance = 1.5f;
	public int specialCubesTotal = 4;
	private const int up = 0;
	private const int down = 1;
	private const int right = 2;
	private const int left = 3;
	private GameObject[,] battleField;
	private GameObject[] specialCubes;
	private bool[] specialCubesHitted;

	void Awake() {
		GenerateMatrix ();
		GenerateSpecial ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int index;

		ColorizeCubes ();
		if(AllSpecialCubesHitted()) {
			Debug.Log ("Coloro tutto di nero!!!!!!!!!!!!!!");
			for (index = 0; index < specialCubes.Length; index++) {
				Debug.Log ("Coloro l'indice " + index);
				ChangeCubeColor(specialCubes[index], Color.black, true);
			}
		}
	}

	private void ColorizeCubes() {
		RaycastHit hit;
		Ray ray;
		Vector3 cubePosition;
		int row;
		int col;
		//int adjacentRow;
		//int adjacentCol;

		if(Input.GetMouseButtonDown(0)) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				cubePosition = hit.transform.localPosition;
				row = (int)(cubePosition.y / cubeDistance);
				col = (int)(cubePosition.x / cubeDistance);
				ChangeCubeColor(battleField [row, col], Color.red, false);
				/*
				for(adjacentRow = row - 1; adjacentRow <= row + 1; adjacentRow++) {
					for(adjacentCol = col - 1; adjacentCol <= col + 1; adjacentCol++) {
						// All index must be inner to matrix
						if((adjacentRow >= 0) && (adjacentCol >= 0) && (adjacentRow < height) && (adjacentCol < width)) {
							// In the central position there is the red cube
							if ((adjacentRow != row) || (adjacentCol != col)) {
								ChangeCubeColor(matrix [adjacentRow, adjacentCol], Color.blue, false);
							}
						}
					}
				}
				*/
			}
		}
	}

	private void ChangeCubeColor(GameObject cube, Color color, bool ignoreSpecial) {
		Renderer cubeRenderer;
		int hittedIndex;

		cubeRenderer = cube.GetComponent<MeshRenderer> ();
		if(IsSpecialCube(cube)) {
			if(ignoreSpecial) {
				cubeRenderer.material.color = color;
			}
			else {
				cubeRenderer.material.color = Color.green;
			}
			hittedIndex = GetSpecialIndex (cube);
			Debug.Log ("hittedIndex: " + hittedIndex);
			specialCubesHitted [hittedIndex] = true;
		}
		else if (cubeRenderer.material.color == Color.white) {
			cubeRenderer.material.color = color;
		}
	}

	private bool AllSpecialCubesHitted() {
		int index;
		bool allHitted;

		for(index = 0, allHitted = true; index < specialCubesHitted.Length; index++) {
			allHitted &= specialCubesHitted [index];
		}
		Debug.Log ("allHitted: " + allHitted);
		return allHitted;
	}

	private int GetSpecialIndex (GameObject cube) {
		int index;
		int cubeIndex;

		cubeIndex = -1;
		for(index = 0; index < specialCubes.Length; index++) {
			if(specialCubes[index] == cube) {
				cubeIndex = index;
			}
		}
		return cubeIndex;
	}

	private void GenerateMatrix() {
		int row;
		int col;

		battleField = new GameObject[height, width];
		for(row = 0; row < height; row++) {
			for(col = 0; col < width; col++) {
				battleField [row, col] = Instantiate (cubePrefab) as GameObject;
				battleField [row, col].transform.parent = parent.transform;
				battleField [row, col].transform.localPosition = new Vector3 (col * cubeDistance, row * cubeDistance, 0);
			}
		}
	}

	private void GenerateSpecial() {
		int startRow;
		int startCol;
		int index;
		int direction;
		bool valid;

		specialCubes = new GameObject[specialCubesTotal];
		specialCubesHitted = new bool[specialCubesTotal];
		do {
			valid = true;
			startRow = Random.Range(0, height);
			startCol = Random.Range(0, width);
			direction = Random.Range (0, 4);
			switch (direction) {
			case up:
				valid = ((startRow - specialCubesTotal + 1) >= 0);
				break;
			case down:
				valid = ((startRow + specialCubesTotal) <= height);
				break;
			case right:
				valid = ((startCol + specialCubesTotal) <= width);
				break;
			case left:
				valid = ((startCol - specialCubesTotal + 1) >= 0);
				break;
			}
		} while(!valid);
		Debug.Log ("direction: " + direction);
		Debug.Log ("startRow: " + startRow + " - startCol: " + startCol);
		Debug.Log ("specialCubesTotal: " + specialCubesTotal);
		for(index = 0; index < specialCubesTotal; index++) {
			switch (direction) {
			case up:
				specialCubes [index] = battleField[startRow - index, startCol];
				Debug.Log ("up row: " + (startRow - index));
				break;
			case down:
				specialCubes [index] = battleField[startRow + index, startCol];
				Debug.Log ("down row: " + (startRow + index));
				break;
			case right:
				specialCubes [index] = battleField[startRow, startCol + index];
				Debug.Log ("right col: " + (startCol + index));
				break;
			case left:
				specialCubes [index] = battleField[startRow, startCol - index];
				Debug.Log ("left col: " + (startCol - index));
				break;
			}
		}
	}

	private bool IsSpecialCube(GameObject cube) {
		bool specialCube;
		int index;

		specialCube = false;
		for(index = 0; index < specialCubes.Length; index++) {
			if((specialCubes[index] != null) && (specialCubes[index] == cube)) {
				specialCube = true;
			}
		}
		return specialCube;
	}
}
