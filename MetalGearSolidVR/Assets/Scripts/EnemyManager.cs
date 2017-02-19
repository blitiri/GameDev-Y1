using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public NavMeshAgent agent;
	public GameObject player;
	public float viewingAngle = 90;
	public float maxLostContactTime = 1;
	private float lostContactStartTime;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private Vector3 lastPatrollingPosition;
	private bool followPlayer;
	private bool goToCamera;
	private bool patrolling;
	private bool backToPosition;
	private GameObject currentCamera;

	// Use this for initialization
	void Start () {
		followPlayer = false;
		goToCamera = false;
		patrolling = true;
		backToPosition = false;
		lastPatrollingPosition = startPosition;
		transform.position = startPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Move() {
		if (followPlayer) {
			agent.destination = player.transform.position;
		}
		else if(goToCamera) {
			agent.destination = currentCamera.transform.position;
			goToCamera = false;
		}
		else if(backToPosition) {
			agent.destination = endPosition;
			lastPatrollingPosition = endPosition;
			backToPosition = false;
		}
		else if(patrolling) {
			if (agent.remainingDistance < 0.5f) {
				if (lastPatrollingPosition == startPosition) {
					if (endPosition != null) {
						agent.destination = endPosition;
						lastPatrollingPosition = endPosition;
					}
				} else if (lastPatrollingPosition == endPosition) {
					agent.destination = startPosition;
					lastPatrollingPosition = startPosition;
				}
			}
		}
	}

	public void FollowPlayer() {
		followPlayer = true;
		goToCamera = false;
		patrolling = false;
		backToPosition = false;
		lostContactStartTime = 0;
	}

	public void ReactToAllarm(GameObject camera) {
		currentCamera = camera;
		followPlayer = false;
		goToCamera = true;
		patrolling = false;
		backToPosition = false;
	}

	public void AlarmCeased() {
		currentCamera = null;
		followPlayer = false;
		goToCamera = false;
		patrolling = false;
		backToPosition = true;
	}

	private bool IsPlayerVisible() {
		return true;
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag.Equals("player")) {
			GameManager.instance.PlayerDied ();
		}
	}

	public void SetStartPosition(Vector3 startPosition) {
		this.startPosition = startPosition;
	}
}
