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
    public LayerMask LongRock;
    public LayerMask Anchor;
    public string Layer;
    public bool isLongRock;

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
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        getInputs();

        // if sprite is within range of move point
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Layer == "Player")
            {   
                movePlayer();
                Animate();
            } else if (Layer == "Pushable" || Layer == "LongRock")
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
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .4f, LongRock)
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
                && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveVert, 0f), .4f, LongRock)
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
            // if rock is less than one tile away from player and more than one tile away
            if (Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .05f, Player))
            {
                // if rock is more than one tile away from obstacle
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor, 0f, 0f), .2f, whatStopsMovement))
                {
                    // only move a long rock if future position is supported by two pushables underneath
                    if (Layer == "LongRock"
                    && Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor - 0.5f, -1f, 0f), .05f, Pushable | LongRock)
                    && Physics2D.OverlapCircle(movePoint.position + new Vector3(moveHor + 0.5f, -1f, 0f), .05f, Pushable | LongRock)
                    && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .05f, LongRock))
                    {
                        // modify move point
                        movePoint.position += new Vector3(moveHor, 0f, 0f);
                    } else if (Layer == "Pushable" && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .05f, LongRock)) // Otherwise, can move pushable rock
                    {
                        // modify move point
                        movePoint.position += new Vector3(moveHor, 0f, 0f);
                    }
                }
            }
        }

        moveRockDown();
    }

    void moveRockDown()
    {
        float tilesFallen = 0; // track the number of tiles a rock has fallen down

        // if the space below a rock is empty, move down one space (gravity)
        while (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .05f, whatStopsMovement)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .05f, Player)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .05f, Pushable)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .05f, LongRock)
            && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .05f, Anchor))
        {
            movePoint.position += new Vector3(0f, -1f, 0f); // move rock down one tile
            tilesFallen++;
        }

        if (tilesFallen >= 2) // player loses if rock falls too far
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("You lost!");
        }
        
    }

    bool gameWon()
    {
        // if the there is nothing in the two tiles above the anchor, game is won
        return (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .05f, Pushable)
                && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 2f, 0f), .05f, Pushable)
                && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .05f, LongRock)
                && !Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 2f, 0f), .05f, LongRock));
    }

}