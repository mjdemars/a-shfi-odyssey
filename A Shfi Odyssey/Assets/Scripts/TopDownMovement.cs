using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{

    public float moveSpeed;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool facingUp = true;
    private float moveHor;
    private float moveVert;

    // awake is called after all objects are initialized. called in a random order.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // nothing yet
    }

    // update is called once per frame, is good for input methods
    void Update()
    {
        //get inputs
        ProcessInputs();

        //animate
        Animate();
    }

    // better for handling physics, can be called multiple times per update frame
    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs() 
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
    }

    private void Animate() 
    {
        if (moveHor > 0 && facingRight) {
            FlipCharacterHor();
        } else if (moveHor < 0 && !facingRight) {
            FlipCharacterHor();
        }

        if (moveVert < 0 && facingUp) {
            FlipCharacterVert();
        } else if (moveVert > 0 && !facingUp) {
            FlipCharacterVert();
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3(moveHor, moveVert, 0f);
        rb.velocity = movement * moveSpeed;
    }

    private void FlipCharacterHor()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void FlipCharacterVert()
    {
        facingUp = !facingUp;
        transform.Rotate(180f, 0f, 0f);
    }
}
