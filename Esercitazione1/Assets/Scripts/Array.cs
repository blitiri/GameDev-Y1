using UnityEngine;
using System.Collections;

public class Array : MonoBehaviour {
	public int exercise = 1;
	public int n = 4;
	public int m = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		switch (exercise) {
		case 1:
			exercise1 ();
			break;
		case 2:
			exercise2 ();
			break;
		case 3:
			exercise3 ();
			break;
		case 4:
			exercise4 ();
			break;
		case 5:
			exercise5 ();
			break;
		case 6:
			exercise6 ();
			break;
		case 7:
			exercise6 ();
			break;
		default:
			Debug.Log ("E allora sei stronzo!");
			break;
		}
	}

	private void exercise1() {
		// Initialize an array of ten int and calculate the average value.
		int[] levelsPoints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		int index;
		float total;

		for (index = 0, total = 0; index < levelsPoints.Length; index++) {
			total += levelsPoints [index];
		}
		Debug.Log("Average: " + total / levelsPoints.Length);
	}
	
	private void exercise2() {
		// Initialize an array of strings and an int N. If the length of a string is bigger than N write "bigger than" + N else write "smaller than" + N.
		string[] names = { "qui", "quo", "qua", "paperino", "topolino", "pippo" };
		int index;

		for (index = 0; index < names.Length; index++) {
			if (names [index].Length > n) {
				Debug.Log(names [index] + " bigger than " + n);
			} else {
				Debug.Log(names [index] + " smaller than " + n);
			}
		}
	}

	private void exercise3() {
		// Initialize an array of ten int and sum them until the sum is odd.
		int[] levelsPoints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		int index;
		int sum;

		sum = 0;
		if (levelsPoints.Length > 0) {
			sum = levelsPoints [0];
			for (index = 1; (index < levelsPoints.Length) && (sum % 2 == 1); index++) {
				sum += levelsPoints [index];
			}
		}
		Debug.Log("Sum: " + sum);
	}

	private void exercise4() {
		// Initialize an int N and another int M, calculate N^M with M > 0 without using Mathf.Pow() function.
		int pow;
		int powIter;

		pow = 1;
		for (powIter = 0; powIter < m; powIter++) {
			pow += multiply(pow, n);
		}
		Debug.Log("Pow: " + pow);
	}

	private void exercise5() {
		// Initialize an array of ten int and sum only even numbers.
		int[] levelsPoints = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		int index;
		int sum;

		for (index = 0, sum = 0; index < levelsPoints.Length; index++) {
			if (levelsPoints [index] % 2 == 0) {
				sum += levelsPoints [index];
			}
		}
		Debug.Log("Sum: " + sum);
	}

	private void exercise6() {
		// Esercizio sul fattoriale (senza uso di moltiplicazioni e trucchetti da hackerz)
		int factorial;
		int partial;
		int currentValue;
		int currentIteration;

		factorial = 1;
		if (n > 1) {
			for(currentValue = 2; currentValue <= n; currentValue++) {
				factorial = multiply (factorial, currentValue);
			}
		}
		Debug.Log ("Factorial: " + factorial);
	}

	private int multiply(int a, int b) {
		int result;
		int iter;

		for(iter = 0, result = 0; iter < a; iter++) {
			result += b;
		}
		return result;
	}

	private void exercise7() {
		int[] firstArray = {3,4,8,2,5,3,1};
		int[] secondArray = {7,2,8,7,5,3,4,5};
		int[] orderedAndMergedArray;
		int insertedElems;

		orderedAndMergedArray = new int[firstArray.Length + secondArray.Length];
		insertedElems = 0;
		insertedElems = insertOrd(firstArray, orderedAndMergedArray, insertedElems);
		insertedElems = insertOrd(secondArray, orderedAndMergedArray, insertedElems);
		Debug.Log ("Ordered and merged array: " + orderedAndMergedArray);
	}

	private int insertOrd(int[] sourceArray, int[] destArray, int insertedElems) {
		int index;
		int insertIndex;

		for(index = 0; index < sourceArray.Length; index++) {
			// Ricerca della posizione
			insertIndex = findInsertPosition(sourceArray[index], destArray, insertedElems);
			shiftElements(destArray, insertIndex);
			destArray[insertIndex] = sourceArray[index];
			insertedElems++;
		}
		return insertedElems;
	}

	private int findInsertPosition(int elem, int[] destArray, int insertedElems) {
		int insertIndex;
		bool found;

		for(insertIndex = 0, found = false; !found && (insertIndex < insertedElems);) {
			if(elem < destArray[insertIndex]) {
				found = true;
			}
			else {
				insertIndex++;
			}
		}
		return insertIndex;
	}

	private void shiftElements(int[] array, int position) {
		int index;

		for(index = array.Length - 1; index > position; index--) {
			array[index] = array[index - 1];
		}
	}
}
