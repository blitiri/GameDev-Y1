using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject battleFieldCellPrefab;
	public GameObject parent;
	public Text messages;
	public int battleFieldHeight = 10;
	public int battleFieldWidth = 10;
	public int[] shipsToPlace;
	public Color initColor = Color.blue;
	public Color hitColor = Color.red;
	public Color notHitColor = Color.gray;
	public Color sunkColor = Color.black;
	public float cellsDistance = 1.5f;
	private GameObject[,] battleField;
	private Ship[] ships;
	private bool running;

	void Awake() {
		running = false;
		try {
			Debug.Log("Validation - Start");
			ValidateShips ();
			Debug.Log("Validation - End");
			Debug.Log("Battle field generation - Start");
			GenerateBattleField ();
			Debug.Log("Battle field generation - End");
			Debug.Log("Ships generation - Start");
			GenerateShips ();
			Debug.Log("Ships generation - End");
			running = true;
		}
		catch(System.Exception ex) {
			messages.text = "Error: " + ex.Message;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray;
		Vector3 cubePosition;
		Coords coords;

		if (running) {
			if (Input.GetMouseButtonDown (0)) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit)) {
					cubePosition = hit.transform.localPosition;
					coords = new Coords ((int)(cubePosition.y / cellsDistance), (int)(cubePosition.x / cellsDistance));
					CheckForHit (coords);
					if (CheckForSunk ()) {
						messages.text = "You Win!!!";
						running = false;
					}
				}
			}
		}
	}
	
	private void ValidateShips() {
		string largerShips;
		string sep;
		int availableSpace;
		int requiredSpace;
		int shipIndex;
		int invalidShips;

		if ((shipsToPlace == null) || (shipsToPlace.Length == 0)) {
			throw new System.Exception ("Set ships to place");
		}
		availableSpace = battleFieldHeight * battleFieldWidth;
		requiredSpace = 0;
		largerShips = "";
		sep = "";
		invalidShips = 0;
		for (shipIndex = 0; shipIndex < shipsToPlace.Length; shipIndex++) {
			requiredSpace += shipsToPlace[shipIndex];
			if ((shipsToPlace [shipIndex] > battleFieldHeight) && (shipsToPlace [shipIndex] > battleFieldWidth)) {
				largerShips += sep + shipIndex;
				sep = ", ";
				invalidShips++;
			}
		}
		if (invalidShips == 1) {
			throw new System.Exception ("Ship " + largerShips + " is larger than battle field size");
		}
		else if (invalidShips > 1) {
			throw new System.Exception ("Ships " + largerShips + " are larger than battle field size");
		}
		if(requiredSpace > availableSpace) {
			throw new System.Exception ("Not enought battle field space to palce all ships");
		}
	}

	private void CheckForHit(Coords coords) {
		int shipIndex;
		bool hit;

		hit = false;
		for (shipIndex = 0; shipIndex < shipsToPlace.Length; shipIndex++) {
			hit |= ships [shipIndex].isHit (coords, hitColor);
		}
		if (!hit) {
			MarkAsWater (coords);
		}
	}

	private void MarkAsWater (Coords coords) {
		GameObject cell;
		Renderer cellRenderer;

		cell = battleField [coords.getRow (), coords.getCol ()];
		cellRenderer = cell.GetComponent<MeshRenderer> ();
		cellRenderer.material.color = notHitColor;
	}

	private bool CheckForSunk() {
		bool allSunk;
		int shipIndex;

		for (shipIndex = 0, allSunk = true; shipIndex < shipsToPlace.Length; shipIndex++) {
			allSunk &= ships [shipIndex].isSunk (sunkColor);
		}
		return allSunk;
	}

	private void GenerateBattleField() {
		Renderer cellRenderer;
		int row;
		int col;

		battleField = new GameObject[battleFieldHeight, battleFieldWidth];
		for(row = 0; row < battleFieldHeight; row++) {
			for(col = 0; col < battleFieldWidth; col++) {
				battleField [row, col] = Instantiate (battleFieldCellPrefab) as GameObject;
				battleField [row, col].transform.parent = parent.transform;
				battleField [row, col].transform.localPosition = new Vector3 (col * cellsDistance, row * cellsDistance, 0);
				cellRenderer = battleField [row, col].GetComponent<MeshRenderer> ();
				cellRenderer.material.color = initColor;
			}
		}
	}

	private void GenerateShips() {
		Ship ship;
		ShipPart[] shipParts;
		ShipPart shipPart;
		CoordsIterator shipCoordsIterator = null;
		int startRow;
		int startCol;
		int shipIndex;
		int otherShipIndex;
		int shipPartIndex;
		int direction;
		int shipSize;
		bool valid;

		ships = new Ship[shipsToPlace.Length];
		for (shipIndex = 0; shipIndex < ships.Length; shipIndex++) {
			shipSize = shipsToPlace [shipIndex];
			Debug.Log ("Find a valid position for ship " + shipIndex);
			// Find a valid start position
			startRow = -1;
			startCol = -1;
			do {
				try {
					startRow = Random.Range (0, battleFieldHeight);
					startCol = Random.Range (0, battleFieldWidth);
					direction = Random.Range (0, 4);
					shipCoordsIterator = CoordsIterator.getIterator(direction, startRow, startCol, shipSize, battleFieldWidth, battleFieldHeight);
					valid = true;
					for(otherShipIndex = 0; otherShipIndex < shipIndex; otherShipIndex++) {
						foreach (Coords shipCoords in shipCoordsIterator)
						{
							Debug.Log("Validate Coords: " + shipCoords);
							valid &= !ships[otherShipIndex].isMyCoord(shipCoords);
						}
					}
				}
				catch(System.Exception) {
					// Not valid star position
					valid = false;
				}
			} while(!valid);
			Debug.Log ("Direction: " + shipCoordsIterator);
			Debug.Log ("Start ship coords: (" + startRow + ", " + startCol + ")");
			Debug.Log ("Place ship " + shipIndex);
			// Place ship
			ship = new Ship();
			shipParts = new ShipPart[shipSize];
			shipPartIndex = 0;
			shipCoordsIterator = shipCoordsIterator.Reset();
			Debug.Log("Ship size: " + shipSize);
			foreach (Coords shipCoords in shipCoordsIterator) {
				Debug.Log("Place Coords: " + shipCoords);
				shipPart = new ShipPart ();
				shipPart.setCoords (shipCoords);
				shipPart.setPart (battleField [shipCoords.getRow(), shipCoords.getCol()]);
				shipParts [shipPartIndex] = shipPart;
				shipPartIndex++;
			}
			ship.setShipParts (shipParts);
			ships [shipIndex] = ship;
		}
	}
}
