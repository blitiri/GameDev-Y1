using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject leftPrefab;
	public GameObject upPrefab;
	public GameObject downPrefab;
	public GameObject rightPrefab;
	public GameObject leftCheckPoint;
	public GameObject upCheckPoint;
	public GameObject downCheckPoint;
	public GameObject rightCheckPoint;
	public GameObject leftSpawn;
	public GameObject upSpawn;
	public GameObject downSpawn;
	public GameObject rightSpawn;
	public CheckPointManager leftCheckPointManager;
	public CheckPointManager upCheckPointManager;
	public CheckPointManager downCheckPointManager;
	public CheckPointManager rightCheckPointManager;
	public Generator leftGenerator;
	public Generator upGenerator;
	public Generator downGenerator;
	public Generator rightGenerator;
	public Text scoreText;
	public Text messagesText;
	public int maxBads = 10;
	public float initialSymbolSpeed = 0.01f;
	public float symbolSpeedIncrease = 5;
	public int oneSymbolProb = 50;
	public int twoSymbolProb = 30;
	public int threeSymbolProb = 15;
	public int fourSymbolProb = 5;
	public float fantasticDistance = .5f;
	public float goodDistance = .1f;
	private const int catchedLayer = 12;
	private const int columnsNumber = 4;
	private KeyCode[] keyCodes = new KeyCode[columnsNumber]{ KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };
	private GameObject[] catchedSymbols = new GameObject[columnsNumber];
	private CheckPointManager[] checkPointManagers = new CheckPointManager[columnsNumber];
	private GameObject[] spawnPoints = new GameObject[columnsNumber];
	private GameObject[] checkPoints = new GameObject[columnsNumber];
	private GameObject[] prefabs = new GameObject[columnsNumber];
	private int maxSymbols;
	private float perc;
	private bool running;
	private int score;
	private float elapsedTime;
	private float textColorLerpStartTime;
	private Color fromTextColor;

	void Awake() {
		int index;

		instance = this;
		checkPointManagers [0] = leftCheckPointManager;
		checkPointManagers [1] = upCheckPointManager;
		checkPointManagers [2] = downCheckPointManager;
		checkPointManagers [3] = rightCheckPointManager;
		for (index = 0; index < checkPointManagers.Length; index++) {
			checkPointManagers [index].fantasticDistance = fantasticDistance;
			checkPointManagers [index].goodDistance = goodDistance;
		}
		spawnPoints [0] = leftSpawn;
		spawnPoints [1] = upSpawn;
		spawnPoints [2] = downSpawn;
		spawnPoints [3] = rightSpawn;
		checkPoints [0] = leftCheckPoint;
		checkPoints [1] = upCheckPoint;
		checkPoints [2] = downCheckPoint;
		checkPoints [3] = rightCheckPoint;
		prefabs [0] = leftPrefab;
		prefabs [1] = upPrefab;
		prefabs [2] = downPrefab;
		prefabs [3] = rightPrefab;
		elapsedTime = Time.time;
	}

	// Use this for initialization
	void Start () {
		running = true;
		maxSymbols = 1;
		score = 0;
		messagesText.text = "";
		fromTextColor = Color.black;
		textColorLerpStartTime = 0;
		UpdateScore ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] catchedSymbols;
		SymbolManager symbolManager;
		int columnIndex;
		int comboLength;

		if (running) {
			catchedSymbols = CheckForCatch ();
			for (columnIndex = 0; columnIndex < columnsNumber; columnIndex++) {
				comboLength = GetComboLength (catchedSymbols [columnIndex]);
				if (comboLength > 1) {
					if (IsComboCatched (catchedSymbols [columnIndex])) {
					}
				} else if (comboLength == 1) {
					symbolManager = catchedSymbols [columnIndex].GetComponent<SymbolManager> ();
					score += (symbolManager.GetCatchLevel () * 10);
					switch (symbolManager.GetCatchLevel ()) {
					case CheckPointManager.fantastic:
						messagesText.text = "Fantastic!!!";
						textColorLerpStartTime = Time.time;
						fromTextColor = new Color (0, 0.7f, 0, 1);
						break;
					case CheckPointManager.good:
						messagesText.text = "Good!";
						textColorLerpStartTime = Time.time;
						fromTextColor = new Color (0, 0, 0.7f, 1);
						break;
					case CheckPointManager.bad:
						messagesText.text = "Bad!";
						textColorLerpStartTime = Time.time;
						fromTextColor = new Color (0.7f, 0, 0, 1);
						break;
					default:
						messagesText.text = "Missing!";
						textColorLerpStartTime = Time.time;
						fromTextColor = new Color (0.7f, 0, 0, 1);
						break;
					}
				}
			}
			LerpTextColor (messagesText, fromTextColor, textColorLerpStartTime);
			GenerateSymbol ();
			UpdateScore ();
		}
	}

	private void LerpTextColor(Text text, Color fromColor, float startTime) {
		Color toColor;

		toColor = new Color (fromColor.r, fromColor.g, fromColor.b, 0);
		text.color = Color.Lerp (fromColor, toColor, (Time.time - startTime) / 2);
	}
	
	public void MissingCatch() {
		score -= 10;
		UpdateScore ();
	}

	private void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	private int GetComboLength(GameObject symbol) {
		return symbol == null ? 0 : 1;
	}

	private bool IsComboCatched(GameObject symbol) {
		return false;
	}

	private void GenerateSymbol() {
		GameObject symbol;
		SymbolManager manager;
		int columnIndex;

		if(elapsedTime > 2) {
			columnIndex = Random.Range (0, columnsNumber);
			symbol = Instantiate (prefabs[columnIndex]) as GameObject;
			symbol.transform.position = spawnPoints[columnIndex].transform.position;
			manager = symbol.GetComponent<SymbolManager> ();
			manager.fromPosition = spawnPoints[columnIndex].transform.position;
			manager.toPosition = checkPoints[columnIndex].transform.position;
			manager.speed = initialSymbolSpeed;
			elapsedTime = 0;
		}
		else {
			elapsedTime += Time.deltaTime;
		}
	}

	private GameObject[] CheckForCatch() {
		int columnIndex;

		for(columnIndex = 0; columnIndex < columnsNumber; columnIndex++) {
			if (Input.GetKeyDown (keyCodes [columnIndex])) {
				catchedSymbols [columnIndex] = checkPointManagers[columnIndex].CheckForCatch ();
				if (catchedSymbols [columnIndex] != null) {
					catchedSymbols [columnIndex].layer = catchedLayer;
				}
			} else {
				catchedSymbols [columnIndex] = null;
			}
		}
		return catchedSymbols;
	}
}
