using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    private float HorizontalMove;
    private float VerticalMove;
    private Rigidbody rb;
    public float rotSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Move();
	}

    public void Move()
    {
        HorizontalMove = Input.GetAxis("Horizontal");
        Vector3 movePositionRight = transform.right * HorizontalMove * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movePositionRight);

        VerticalMove = Input.GetAxis("Vertical");
        Vector3 movePositionForward = transform.forward * VerticalMove * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movePositionForward);

        //Turn
        Quaternion turnRotation = Quaternion.Euler(0f, HorizontalMove * rotSpeed * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
