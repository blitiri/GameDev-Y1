using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public bool canMove;

    public int bombNumber = 0;
    public int bombLimit = 1;

    private float speed = 5;

    public GameObject bombPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        canMove = MoveCondition();
    }

    public bool MoveCondition()
    {
        Vector3 ray = transform.position;
        if (Physics.Raycast(ray, Vector3.forward, transform.localScale.z / 2 + 0.1f))
            return true;

        return false;
    }

    public void Move(Vector3 direction)
    {
        if (!canMove)
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void BombThrower()
    {
		BombManager manager;

        if (bombNumber < bombLimit)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation) as GameObject;
            bomb.transform.position = new Vector3(Mathf.RoundToInt(bomb.transform.position.x), bomb.transform.position.y, Mathf.RoundToInt(bomb.transform.position.z));
			manager = bomb.GetComponent<BombManager> ();
			manager.player = this;
            bombNumber++;
            Debug.Log(bombNumber);
        }
    }
}
