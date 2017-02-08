using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public GameObject codePegPrefab;
	public GameObject keyPegPrefab;
	public GameObject checkCodeButtonPrefab;
	public GameObject codeMaskPrefab;
	public GameObject winnerText;
	public GameObject looserText;
	public float codePegSpacing = 2;
	public float keyPegSpacing = .3f;
	public float rowSpacing = 1.5f;
	public float checkButtonSpacing = 3;
	public float codeRowMargin = 1;
	public float startPosition = 10;
	public Color noneColor = Color.white;
	public Color okColor = Color.green;
	public Color koColor = Color.red;
	private const int notSet = -1;
	private int codeLength = 4;
	[Range(1, 10)]
	public int maxAttempts = 10;
	[Range(1, 11)]
	public int maxValues = 4;
	private int attempt;
	private int[] code;
	private int[,] attempts;
	private int[,] feedbaks;
	private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.black, Color.gray, Color.white, Color.clear, Color.cyan, Color.magenta, Color.magenta };
	private GameObject[] codePegs;
	private GameObject[,] attemptsPegs;
	private GameObject[] checkButtons;
	private GameObject[,] feedbaksPegs;
	private GameObject codeMask = null;

	// Inits all variables
	void Awake() {
		instance = this;
		code = new int[codeLength];
		attempts = new int[maxAttempts, codeLength];
		feedbaks = new int[maxAttempts, codeLength];
		codePegs = new GameObject[codeLength];
		attemptsPegs = new GameObject[maxAttempts, codeLength];
		checkButtons = new GameObject[maxAttempts];
		feedbaksPegs = new GameObject[maxAttempts, codeLength];
		checkButtons = new GameObject[maxAttempts];
	}

	// Start the game
	void Start() {
		StartNewGame ();
	}

	// Get the color for the current attempt
	public Color GetAttemptColor(int attemptIndex, int colorIndex) {
		colorIndex = colorIndex % maxValues;
		attempts [attempt, attemptIndex] = colorIndex;
		return colors [colorIndex];
	}

	// Verify if user have compleated input
	public bool IsAttemptComplete() {
		bool complete;
		int index;

		complete = true;
		for(index = 0; index < codeLength; index++) {
			complete &= (attempts [attempt, index] > notSet);
		}
		return complete;
	}

	// Verify if attempt match code
	public void CheckAttempt() {
		MeshRenderer feedbaksPegsRenderer;
		bool currentMatch;
		bool match;
		int index;

		match = true;
		for(index = 0; index < codeLength; index++) {
			currentMatch = (attempts [attempt, index] == code [index]);
			feedbaksPegsRenderer = feedbaksPegs [attempt, index].GetComponent<MeshRenderer> ();
			feedbaksPegsRenderer.material.color = currentMatch ? okColor : koColor;
			match &= currentMatch;
		}
		Destroy (checkButtons [attempt]);
		if(match) {
			AndTheWinnerIs ();
		}
		else {
			attempt++;
			if(attempt == maxAttempts) {
				YouAreALooser ();
			}
			else {
				GenerateRow (GetPositionForRow(attempt));
			}
		}
	}

	// Start a new game
	private void StartNewGame() {
		ChooseCode();
		GenerateCodeRow ();
		attempt = 0;
		GenerateRow (GetPositionForRow(attempt));
	}

	// Choose a new secret code
	private void ChooseCode() {
		int index;

		for(index = 0; index < codeLength; index++) {
			code [index] = Random.Range (0, codeLength);
		}
	}

	// Generate the code row
	private void GenerateCodeRow() {
		Vector3 rowPosition;

		rowPosition = new Vector3 (0, startPosition, 0);
		GeneratePegs (rowPosition, false);
		codeMask = Instantiate (codeMaskPrefab);
		rowPosition.x += 2.5f;
		codeMask.transform.Translate (rowPosition, Space.World);
	}

	// Generate an empty attempt row
	private void GenerateRow(Vector3 rowPosition) {
		GenerateAttempt (rowPosition);
		GenerateCheckCodeButton (rowPosition);
		GenerateFeedbacks (rowPosition);
		InitAttempts ();
	}

	// Get the position for the current row
	private Vector3 GetPositionForRow(int row) {
		Vector3 rowPosition;

		rowPosition = new Vector3 (0, startPosition - rowSpacing * (row + 1) - codeRowMargin, 0);
		return rowPosition;
	}

	// 
	private void InitAttempts() {
		int index;

		for(index = 0; index < codeLength; index++) {
			attempts [attempt, index] = notSet;
		}
	}

	private void GeneratePegs(Vector3 rowPosition, bool attemptRow) {
		Renderer pegRenderer = null;
		Vector3 position;
		int pegIndex;

		position = new Vector3(rowPosition.x, rowPosition.y, rowPosition.z);
		for(pegIndex = 0; pegIndex < codeLength; pegIndex++) {
			codePegs [pegIndex] = Instantiate (codePegPrefab) as GameObject;
			pegRenderer = codePegs [pegIndex].GetComponent<Renderer> ();
			pegRenderer.material.color = (attemptRow ? Color.white : colors [code [pegIndex]]);
			position.x = position.x + codePegSpacing;
			codePegs [pegIndex].transform.Translate (position, Space.World);
			if(attemptRow) {
				codePegs [pegIndex].tag = pegIndex + "";
			}
			else {
				codePegs [pegIndex].tag = "code";
			}
		}
	}

	private void GenerateAttempt(Vector3 rowPosition) {
		GeneratePegs (rowPosition, true);
	}

	private void GenerateCheckCodeButton(Vector3 rowPosition) {
		Vector3 position;

		checkButtons [attempt] = Instantiate (checkCodeButtonPrefab) as GameObject;
		position = new Vector3 (rowPosition.x + codePegSpacing * codeLength + checkButtonSpacing, rowPosition.y, rowPosition.z);
		checkButtons [attempt].transform.Translate (position, Space.World);
	}

	private void GenerateFeedbacks(Vector3 rowPosition) {
		int feedbackIndex;
		float baseX;
		float halfKeyPegSpacing;

		feedbackIndex = 0;
		halfKeyPegSpacing = keyPegSpacing / 2;
		baseX = rowPosition.x + codePegSpacing * codeLength + checkButtonSpacing + keyPegSpacing + .5f;
		GenerateFeedback (feedbackIndex++, new Vector3 (baseX, rowPosition.y + halfKeyPegSpacing, rowPosition.z));
		GenerateFeedback (feedbackIndex++, new Vector3 (baseX + keyPegSpacing, rowPosition.y + halfKeyPegSpacing, rowPosition.z));
		GenerateFeedback (feedbackIndex++, new Vector3 (baseX, rowPosition.y - halfKeyPegSpacing, rowPosition.z));
		GenerateFeedback (feedbackIndex++, new Vector3 (baseX + keyPegSpacing, rowPosition.y - halfKeyPegSpacing, rowPosition.z));
	}

	private void GenerateFeedback(int feedbackIndex, Vector3 position) {
		Renderer feedbackRenderer = null;

		feedbaksPegs [attempt, feedbackIndex] = Instantiate (keyPegPrefab) as GameObject;
		feedbaksPegs [attempt, feedbackIndex].transform.Translate (position, Space.World);
		feedbackRenderer = feedbaksPegs [attempt, feedbackIndex].GetComponent<Renderer> ();
		feedbackRenderer.material.color = noneColor;
	}

	private void AndTheWinnerIs() {
		Destroy (codeMask);
		winnerText.SetActive(true);
	}

	private void YouAreALooser() {
		Destroy (codeMask);
		looserText.SetActive(true);
	}
}
