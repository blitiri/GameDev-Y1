using UnityEngine;
using System.Collections;

public class BombManager : MonoBehaviour {
	[HideInInspector]
    public PlayerManager player;
	public float countDown = 3;
	[SerializeField]
	private int flameLen = 10;
	public const int north = 0;
	public const int south = 1;
	public const int west = 2;
	public const int east = 3;
	public float flameAnimationLen = 2.5f;
	public GameObject flamePrefab;
	public GameObject flameCenterPrefab;
	private Renderer bombRenderer;
	private Ray[] checkRays = new Ray[4];

	void Awake() {
		bombRenderer = gameObject.GetComponent<MeshRenderer> ();
	}

	// Use this for initialization
	void Start () {
		setFlameLen (flameLen);
		StartCoroutine(CountDown());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setFlameLen(int flameLen) {
		this.flameLen = flameLen;
		checkRays[north] = GetNorthRay ();
		checkRays[south] = GetSouthRay ();
		checkRays[west] = GetWestRay ();
		checkRays[east] = GetEastRay ();
	}

	private IEnumerator CountDown() {
		yield return new WaitForSeconds (countDown / 2);
		NearlyToExplode ();
		yield return new WaitForSeconds (countDown / 2);
		Explode ();
	}

	private void NearlyToExplode () {
		Renderer childRenderer;

		bombRenderer.material.color = Color.red;
		foreach (Transform child in transform)
		{
			childRenderer = child.gameObject.GetComponent<MeshRenderer> ();
			childRenderer.material.color = Color.red;
		}
	}

	private void Explode() {
		RaycastHit hit;
		Ray ray;
		GameObject[,] flames;
		bool hitted;
		int rayIndex;
		int direction;
		int flameIndex;

		StartExplosion ();
		flames = new GameObject[4, flameLen];
		for(rayIndex = 0; rayIndex < checkRays.Length; rayIndex++) {
			ray = checkRays [rayIndex];
			Debug.DrawRay (ray.origin, ray.direction * flameLen, Color.magenta, 100);
			hitted = Physics.Raycast (ray, out hit, flameLen);
			if (hitted && !hit.transform.gameObject.tag.Equals("Untagged")) {
				GameManager.instance.DestroyObject (hit.transform.gameObject, player.gameObject);
			}
			GenerateFlames (ray.origin, hitted ? Mathf.RoundToInt (hit.distance) : flameLen, rayIndex, flames);
		}
		for(flameIndex = 0; flameIndex < flameLen; flameIndex++) {
			for(direction = north; direction <= east; direction++) {
				if (flames [direction, flameIndex] != null) {
					Destroy (flames [direction, flameIndex], (flameAnimationLen - .5f) - ((flameAnimationLen - .5f) / flameLen * flameIndex));
				}
			}
		}
	}

	private Ray GetNorthRay() {
		Ray nRay;
		Vector3 position;

		position = gameObject.transform.position;
		nRay = new Ray ();
		nRay.direction = Vector3.forward * flameLen;
		nRay.origin = new Vector3(position.x, position.y, position.z + .4f);
		return nRay;
	}

	private Ray GetSouthRay() {
		Ray sRay;
		Vector3 position;

		position = gameObject.transform.position;
		sRay = new Ray ();
		sRay.direction = Vector3.back * flameLen;
		sRay.origin = new Vector3(position.x, position.y, position.z - .4f);
		return sRay;
	}

	private Ray GetWestRay() {
		Ray wRay;
		Vector3 position;

		position = gameObject.transform.position;
		wRay = new Ray ();
		wRay.direction = Vector3.right * flameLen;
		wRay.origin = new Vector3(position.x + .4f, position.y, position.z);
		return wRay;
	}

	private Ray GetEastRay() {
		Ray eRay;
		Vector3 position;

		position = gameObject.transform.position;
		eRay = new Ray ();
		eRay.direction = Vector3.left * flameLen;
		eRay.origin = new Vector3(position.x - .4f, position.y, position.z);
		return eRay;
	}

	private void StartExplosion() {
		GameObject explosion;

		explosion = Instantiate (flameCenterPrefab) as GameObject;
		explosion.transform.position = gameObject.transform.position;
		Destroy (gameObject);
		Destroy (explosion, flameAnimationLen);
	}

	private void GenerateFlames(Vector3 flameStart, int flameLen, int direction, GameObject[,] flames) {
		GameObject flame;
		Vector3 position;
		int index;

		position = new Vector3 ();
		position.x = Mathf.Round(flameStart.x);
		position.y = Mathf.Round(flameStart.y);
		position.z = Mathf.Round(flameStart.z);
		for (index = 0; index < flameLen; index++) {
			flame = Instantiate (flamePrefab) as GameObject;
			switch (direction) {
			case north:
				position.z++;
				break;
			case south:
				position.z--;
				break;
			case west:
				position.x++;
				break;
			case east:
				position.x--;
				break;
			}
			flame.transform.position = position;
			flames [direction, index] = flame;
		}
	}
	
    void OnDestroy()
    {
		player.bombNumber--;
    }
}
