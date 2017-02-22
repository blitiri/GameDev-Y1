using UnityEngine;
using System.Collections;

public class EnemyManager : PlayerDetector {
	public NavMeshAgent agent;
	public float lostContactMaxTime = 1;
	private float lostContactStartTime;
	private Transform startPosition;
	private Transform endPosition;
	private Transform lastPatrollingPosition;
	private bool followPlayer;
	private bool goToCamera;
	private bool patrolling;
	private bool backToPosition;
	private bool inited;
	private GameObject currentCamera;

	void Awake() {
		inited = false;
	}

	void Update () {
		if (inited) {
			Move ();
		}
	}

	public void Init(GameObject player, Transform startPosition, Transform endPosition) {
		followPlayer = false;
		goToCamera = false;
		patrolling = true;
		backToPosition = false;
		this.startPosition = startPosition;
		this.endPosition = endPosition;
		lastPatrollingPosition = this.startPosition;
		transform.position = this.startPosition.position;
		agent.destination = this.startPosition.position;
		lostContactStartTime = -1;
		this.player = player;
		inited = true;
	}

	private void Move() {
		if (followPlayer) {
			agent.destination = player.transform.position;
			if (!IsPlayerVisible (gameObject.transform, transform.forward)) {
				if (lostContactStartTime < 0) {
					lostContactStartTime = Time.time;
				}
				if (Time.time - lostContactStartTime > lostContactMaxTime) {
					AlarmCeased ();
				}
			}
		}
		else if(goToCamera) {
			agent.destination = currentCamera.transform.position;
			goToCamera = false;
		}
		else if(backToPosition) {
			agent.destination = startPosition.position;
			lastPatrollingPosition = startPosition;
			Patrolling ();
		}
		else if(patrolling) {
			if (agent.remainingDistance < 0.5f) {
				if (lastPatrollingPosition.Equals(startPosition)) {
					if (endPosition != null) {
						agent.destination = endPosition.position;
						lastPatrollingPosition = endPosition;
					}
				} else if (lastPatrollingPosition.Equals(endPosition)) {
					agent.destination = startPosition.position;
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
		lostContactStartTime = -1;
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

	public void Patrolling() {
		followPlayer = false;
		goToCamera = false;
		patrolling = true;
		backToPosition = false;
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag.Equals("Player")) {
			GameManager.instance.PlayerDied ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			if (IsPlayerVisible (gameObject.transform, transform.forward)) {
				FollowPlayer ();
			}
		}
	}
}
