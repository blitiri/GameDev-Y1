using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
	public float movementSpeed = 500;
	public float jumpSpeed = 12;
	public float maxSpeed = 15;
	public float cameraPlacerFactor = 1;
	public float maxCameraPlacerDeltaX = 10;
	public float maxCameraPlacerDeltaY = 5;
	public Transform cameraPlacerTransform;
	public Transform background3;
	public Transform background2;
	public Transform background1;
	public float movement3XFactor = 0.9f;
	public float movement2XFactor = 0.45f;
	public float movement1XFactor = 0.2f;
	private Rigidbody2D playerRigidbodyInstance = null;
	private Rigidbody2D playerRigidbody
	{
		get
		{
			return playerRigidbodyInstance ?? (playerRigidbodyInstance = gameObject.GetComponent<Rigidbody2D> ());
		}
	}
	private Animator playerAnimatorInstance = null;
	private Animator playerAnimator
	{
		get
		{
			return playerAnimatorInstance ?? (playerAnimatorInstance = gameObject.GetComponent<Animator> ());
		}
	}
	private SpriteRenderer playerSpriteRendererInstance = null;
	private SpriteRenderer playerSpriteRenderer
	{
		get
		{
			return playerSpriteRendererInstance ?? (playerSpriteRendererInstance = gameObject.GetComponent<SpriteRenderer> ());
		}
	}
	private BoxCollider2D playerColliderInstance = null;
	private BoxCollider2D playerCollider
	{
		get
		{
			return playerColliderInstance ?? (playerColliderInstance = gameObject.GetComponent<BoxCollider2D> ());
		}
	}
	private Vector3 lastPlayerVelocity;
	private bool jumping;
	private bool attack;
	private const float stopLimit = 0.3f;
	private float startCameraPlacerX;
	private bool active;

	void Awake() {
		lastPlayerVelocity = playerRigidbody.velocity;
		jumping = false;
		attack = false;
		active = true;
		startCameraPlacerX = cameraPlacerTransform.position.x;
	}

	// Update is called once per frame
	void Update () {
		if (active) {
			CheckJump ();
			Move ();
		}
		AnimatePlayer ();
		UpdateCameraPlacer ();
		AnimateBackgrounds ();
	}

	void LateUpdate() {
	}

	private void CheckJump () {
		Vector3 colliderCenter;
		Vector3 colliderExtents;
		Vector3 leftRayPosition;
		Vector3 rightRayPosition;

		if (jumping) {
			colliderCenter = playerCollider.bounds.center;
			colliderExtents = playerCollider.bounds.extents;
			leftRayPosition = new Vector3 (colliderCenter.x - colliderExtents.x, colliderCenter.y - colliderExtents.y, colliderCenter.z);
			Debug.DrawRay (leftRayPosition, Vector3.down, Color.red, 0.2f);
			rightRayPosition = new Vector3 (colliderCenter.x + colliderExtents.x, colliderCenter.y - colliderExtents.y, colliderCenter.z);
			Debug.DrawRay (rightRayPosition, Vector3.down, Color.blue, 0.2f);
			Debug.Log ("Raycast sinistro: " + Physics2D.Raycast (leftRayPosition, Vector2.down, 1f));
			Debug.Log ("Raycast destro: " + Physics2D.Raycast (rightRayPosition, Vector2.down, 1f));
			if(Physics2D.Raycast (leftRayPosition, Vector2.down, 1f) || Physics2D.Raycast (rightRayPosition, Vector2.down, 1f)) {
				jumping = false;
			}
		}
	}

	private void Move() {
		Vector2 newColliderSize;

		if(Input.GetKey(KeyCode.LeftArrow)) {
			playerRigidbody.AddForce (Vector2.left * movementSpeed * Time.deltaTime);
			playerSpriteRenderer.flipX = true;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			playerRigidbody.AddForce (Vector2.right * movementSpeed * Time.deltaTime);
			playerSpriteRenderer.flipX = false;
		}
		if (Input.GetKey (KeyCode.Space)) {
			newColliderSize = new Vector2 (2.56f, playerCollider.size.y);
			playerCollider.size = newColliderSize;
			attack = true;
		}
		if(!jumping && Input.GetKeyDown(KeyCode.UpArrow)) {
			playerRigidbody.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			jumping = true;
		}
		playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity, maxSpeed);
	}

	private void AnimatePlayer() {
		Vector2 newColliderSize;

		if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			Debug.Log ("Passato di qui");
			newColliderSize = new Vector2 (1.3f, playerCollider.size.y);
			playerCollider.size = newColliderSize;
			attack = false;
		}
		if (active) {
			playerAnimator.SetFloat ("velocity", playerRigidbody.velocity.x);
		} else {
			playerAnimator.SetFloat ("velocity", 0);
		}
		playerAnimator.SetBool ("jumping", jumping);
		playerAnimator.SetBool ("attack", attack);
		playerAnimator.SetBool ("active", active);
	}

	private void AnimateBackgrounds() {
		AnimateBackground (background3, movement3XFactor);
		AnimateBackground (background2, movement2XFactor);
		AnimateBackground (background1, movement1XFactor);
	}

	private void AnimateBackground(Transform background, float movementFactor) {
		Vector3 backgroundPosition;
		float xPosition;
		float movement;

		if (background) {
			xPosition = GetCameraPivotPosition().x;
			movement = (xPosition - startCameraPlacerX) * movementFactor;
			backgroundPosition = new Vector3 (movement - 10, background.position.y, background.position.z);
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

	public Vector3 GetCameraPivotPosition() {
		return cameraPlacerTransform.position;
	}

	public void setActive(bool active) {
		this.active = active;
		playerCollider.isTrigger = !active;
		if (!active) {
			jumping = false;
			attack = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		string otherTag;

		if (active) {
			otherTag = collision.gameObject.tag;
			if (otherTag.Equals ("Coin")) {
				GameManager.instance.CoinPicked (collision.gameObject);
			}
			if (otherTag.Equals ("Ghost")) {
				if (attack) {
					GameManager.instance.GhostPicked (collision.gameObject);
				} else {
					GameManager.instance.GhostTouched ();
				}
			}
		}
	}
}
