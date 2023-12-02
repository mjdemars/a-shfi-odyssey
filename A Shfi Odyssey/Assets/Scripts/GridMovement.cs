using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed;
    public Transform movePoint;

    public LayerMask whatStopsMovement;

    private bool facingRight = true;
    private float moveHor;
    private float moveVert;

    // called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // if player is within range of move point
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            movePlayer();
            Animate();
        }
    }

    void movePlayer()
    {
        // if horizontal input detected
        if (Mathf.Abs(moveHor) == 1f)
        {
            // if player is more than one tile away from obstacle
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .2f, whatStopsMovement))
            {
                // modify move point
                movePoint.position += new Vector3(moveHor, 0f, 0f);
            }
        } else if (Mathf.Abs(moveVert) == 1f) // if vertical input detected
        {
            // if player is more than one tile away from obstacle
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveVert, 0f), .2f, whatStopsMovement))
            {
                // modify move point
                movePoint.position += new Vector3(0f, moveVert, 0f);
            }
        }
    }

    private void Animate() 
    {
        if (moveHor > 0 && facingRight) {
            FlipCharacterHor();
        } else if (moveHor < 0 && !facingRight) {
            FlipCharacterHor();
        }
    }

    private void FlipCharacterHor()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f, Space.Self);
    }

}
