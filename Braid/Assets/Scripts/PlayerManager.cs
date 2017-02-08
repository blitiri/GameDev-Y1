using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float speed = 100;
	public float jump = 10;
	public float maxSpeed = 15;
	public float movementSpeed = 500;
	public float jumpSpeed = 12;
	public float cameraPlacerFactor = 1;
	public float maxCameraPlacerDeltaX = 10;
	public float maxCameraPlacerDeltaY = 5;
	public Transform cameraPlacerTransform;
	public Transform backBackground;
	public Transform middleBackground;
	public Transform frontBackground;
	public float backMovementFactor = 0.9f;
	public float middleMovementFactor = 0.45f;
	public float frontMovementFactor = 0.2f;
	private const float stopLimit = 0.3f;
	private Rigidbody2D playerRigidbody;
	private SpriteRenderer playerSpriteRenderer;
	private Vector3 lastPlayerVelocity;
	private bool jumping;
	private float startCameraPlacerX;
	private Animator playerAnimator;

	void Awake() {
		playerAnimator = gameObject.GetComponent<Animator> ();
		playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		playerRigidbody = gameObject.GetComponent<Rigidbody2D> ();
		lastPlayerVelocity = playerRigidbody.velocity;
		jumping = false;
		startCameraPlacerX = cameraPlacerTransform.position.x;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		UpdateCameraPlacer ();
		AnimateBackgrounds ();
		UpdateAnimation ();
	}

	private void UpdateAnimation() {
		playerAnimator.SetFloat ("velocity", playerRigidbody.velocity.x);
		playerAnimator.SetBool ("jumping", Mathf.Abs(playerRigidbody.velocity.y) > 0.1);
		playerSpriteRenderer.flipX = (playerRigidbody.velocity.x < 0);
	}

	private void Move() {
		Vector2 force;
		float movement;

		/*
		if(Input.GetKey(KeyCode.LeftArrow)) {
			playerRigidbody.AddForce (Vector2.left * movementSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			playerRigidbody.AddForce (Vector2.right * movementSpeed * Time.deltaTime);
		}
		if(!jumping && Input.GetKeyDown(KeyCode.UpArrow)) {
			playerRigidbody.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			jumping = true;
		}
		playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity, maxSpeed);

				 */ 

		movement = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (movement) > 0.1) {
			force = new Vector2 (movement * speed, 0);
			playerRigidbody.AddForce (force);
		}
		if (Mathf.Abs (playerRigidbody.velocity.y) < 0.01) {
			movement = Input.GetAxis ("Vertical");
			if (Mathf.Abs (movement) > 0.1) {
				force = new Vector2 (0, movement * jump);
				playerRigidbody.AddForce (force, ForceMode2D.Impulse);
			}
		}
		playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity, maxSpeed);
	}

	private void AnimateBackgrounds() {
		AnimateBackground (backBackground, backMovementFactor);
		AnimateBackground (middleBackground, middleMovementFactor);
		AnimateBackground (frontBackground, frontMovementFactor);
	}

	private void AnimateBackground(Transform background, float movementFactor) {
		Vector3 backgroundPosition;
		float xPosition;
		float movement;

		if (background) {
			xPosition = GetCameraPosition().x;
			movement = (xPosition - startCameraPlacerX) * movementFactor;
			backgroundPosition = new Vector3 (movement, background.position.y, background.position.z);
			background.position = backgroundPosition;
		}
	}

	private void UpdateCameraPlacer () {
		Vector3 cameraPlacerPosition;

		lastPlayerVelocity = Vector3.Lerp(lastPlayerVelocity, playerRigidbody.velocity, 1f * Time.deltaTime);
		cameraPlacerTransform.localPosition = new Vector3(0, 0, 0);
		cameraPlacerTransform.Translate (lastPlayerVelocity * cameraPlacerFactor);
		cameraPlacerPosition = cameraPlacerTransform.localPosition;
		cameraPlacerPosition.x = Mathf.Clamp (cameraPlacerPosition.x, -maxCameraPlacerDeltaX, maxCameraPlacerDeltaX);
		cameraPlacerPosition.y = Mathf.Clamp (cameraPlacerPosition.y, -maxCameraPlacerDeltaY, maxCameraPlacerDeltaY);
		cameraPlacerTransform.localPosition = cameraPlacerPosition;
	}

	public Vector3 GetCameraPosition() {
		return cameraPlacerTransform.position;
	}

	public bool IsJumping() {
		return jumping;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		jumping = false;
	}
}
