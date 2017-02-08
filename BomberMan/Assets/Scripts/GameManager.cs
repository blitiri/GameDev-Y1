using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public float destroyAnimationLen = 2f;

    public PlayerManager p1;
    public PlayerManager p2;

	private float[] scores = {0,0,0,0};

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer1 ();
		MovePlayer2 ();
		Debug.Log("Player1: " + scores[0] + "  Player2: " + scores[1] + "  Player3: " + scores[2] + "  Player4: " + scores[3]);
    }

	public void AddPoints(int points, GameObject player) {
		if(player.tag.Equals("Player1")) {
			scores [0] += points;
		}
		else if(player.tag.Equals("Player2")) {
			scores [1] += points;
		}
		else if(player.tag.Equals("Player3")) {
			scores [2] += points;
		}
		else if(player.tag.Equals("Player4")) {
			scores [3] += points;
		}
	}

	private void MovePlayer1() {
		if (Input.GetKey(KeyCode.W))
		{
			p1.Move(Vector3.forward);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			p1.Move(Vector3.right);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			p1.Move(Vector3.back);
		} 
		else if (Input.GetKey(KeyCode.A))
		{
			p1.Move(Vector3.left);
		}
		if (Input.GetKeyUp(KeyCode.Space))
		{
			p1.BombThrower();
		}
	}

	private void MovePlayer2() {
		if (Input.GetKey(KeyCode.UpArrow))
		{
			p2.Move(Vector3.forward);
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			p2.Move(Vector3.right);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			p2.Move(Vector3.back);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			p2.Move(Vector3.left);
		}
		if (Input.GetKeyUp(KeyCode.KeypadEnter))
		{
			p2.BombThrower();
		}
	}

	public void DestroyObject(GameObject toDestroy, GameObject player) {
		DestroyableWallManager manager = null;

		if(!toDestroy.tag.Equals("Player")) {
			Debug.Log ("Imposto il player al muro da distruggere");
			manager = toDestroy.GetComponent<DestroyableWallManager> ();
			manager.player = player;
		}
		StartCoroutine(DestroyAnimation(toDestroy));
	}

	private IEnumerator DestroyAnimation(GameObject toDestroy) {
		Renderer toDestroyRenderer;

		toDestroyRenderer = toDestroy.GetComponent<MeshRenderer> ();
		toDestroyRenderer.material.color = Color.gray;
		yield return new WaitForSeconds (destroyAnimationLen / 2);
		toDestroyRenderer.material.color = Color.black;
		yield return new WaitForSeconds (destroyAnimationLen / 2);
		Destroy (toDestroy);
	}
}
