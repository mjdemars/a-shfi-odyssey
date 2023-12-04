using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGridElement : MonoBehaviour
{
    // set in Unity
    public float moveSpeed;
    public Transform movePoint;
    public LayerMask whatStopsMovement;
    public LayerMask Player;
    public LayerMask Pushable;
    public LayerMask Anchor;
    public string Layer;

    // detected movement
    private float moveHor;
    private float moveVert;

    private bool facingRight = true;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        getInputs();

        // if sprite is within range of move point
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Layer == "Player")
            {   
                movePlayer();
                Animate();
            } else if (Layer == "Pushable")
            {
                moveRock();
            }
        }

        if (Layer == "Anchor")
        {
            if (gameWon()) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        
    }

    void getInputs()
    {
        moveHor = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    void Animate() 
    {
        if (moveHor > 0 && facingRight) {
            FlipCharacterHor();
        } else if (moveHor < 0 && !facingRight) {
            FlipCharacterHor();
        }
    }

    void FlipCharacterHor()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f, Space.Self);
    }

    void movePlayer()
    {
        // if horizontal input detected
        if (Mathf.Abs(moveHor) == 1f)
        {
            // if player is more than one tile away from obstacle
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .2f, whatStopsMovement)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .4f, Pushable)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .4f, Anchor))
            {
                // modify move point
                movePoint.position += new Vector3(moveHor, 0f, 0f);
            }
        } else if (Mathf.Abs(moveVert) == 1f) // if vertical input detected
        {
            // if player is more than one tile away from obstacle
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveVert, 0f), .2f, whatStopsMovement)
                && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveVert, 0f), .4f, Pushable)
                && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveVert, 0f), .4f, Anchor))
            {
                // modify move point
                movePoint.position += new Vector3(0f, moveVert, 0f);
            }
        }
    }

    void moveRock()
    {
        if (Mathf.Abs(moveHor) == 1f) // rock only moves if player pushes or pulls from a horizontally adjacent tile
        {
            // if rock is less than one tile away from player
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .05f, Player))
            {
                // if rock is more than one tile away from obstacle
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .2f, whatStopsMovement))
                {
                    // modify move point
                    movePoint.position += new Vector3(moveHor, 0f, 0f);
                }
            }
        }
        // if the space below a rock is empty, move down one space (gravity)
        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .5f, whatStopsMovement)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .5f, Player)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .5f, Pushable)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .5f, Anchor))
        {
            // modify move point
            movePoint.position += new Vector3(0f, -1f, 0f);
        }
    }

    bool gameWon()
    {
        // if the there is nothing in the tile above the anchor, game is won
        return (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .05f, Pushable));
    }

}