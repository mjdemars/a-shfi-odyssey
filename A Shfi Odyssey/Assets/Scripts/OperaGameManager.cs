using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OperaGameManager : MonoBehaviour
{
    //holds the four game objects representative of the arros
    public GameObject[] directionSprites;

    public GameObject player;
    //
    private int dirSelect;
    
    //in charge of the time that arrows stay on and off when being presented
    public float stayLitTime;
    public float offTime;
    
    //the timer that tracks this
    private float glowTimer;

    //flag variable that indicates when it's time to switch arrows
    private bool anArrowGlows;

    //flag variable that just indicates when we're in the off period between arrow glows
    private bool offSequence;

    //keeps 
    int sequenceTracker = 0;

    private float gTimer = 0;

    //variable that shows when it's time for player to be able to respond
    private bool letPlayerRespond = false;

    private Vector3 MovementVector;

    //enum of directions-useful for management
    private enum dir
    {
        Up, Down, Left, Right
    }
    //variable storing how many rounds we want to have
    public int totalRounds = 10;

    //variable for storing what round we're currently on
    private int currentRound = 1;

    //variable for movement strength
    public float originalMovementStrength;

    private float movementStrength;

    
    //variable for gravityStrength
    public float gravityStrength;

    //the list that contains the total directions sequence of the minigame
    List<dir> directionsPattern = new List<dir>();

    //the list that has all the player's inputs. resets with each round
    List<dir> playerPattern = new List<dir>();

    // Start is called before the first frame update
    void Start()
    {
        //add the pattern to the directionPattern list
        generateList();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();

        if (letPlayerRespond)
        {
            if (playerPattern.Count < currentRound)
            {
                playerController();
            }
            
        } else
        {
            directionGlowController();
        }

    }

    

    void generateList()
    {
        for (int i = 0; i < totalRounds; i++)
        {
            dir randomValue = (dir)UnityEngine.Random.Range(0, Enum.GetValues(typeof(dir)).Length);
            UnityEngine.Debug.Log("i is " + i + " and we're about to add: " + randomValue);
            directionsPattern.Add(randomValue);
        }
    }


    //method that sets the isGlowing parameter of a direction and a boolean glowing flag variable to glow state
    void showArrow(dir direction, bool glow)
    {
        //UnityEngine.Debug.Log("Turning " + direction + " " + glow);
        Animator animatorControl = directionSprites[(int)direction].GetComponent<Animator>();
        animatorControl.SetBool("isGlowing", glow);

        offSequence = !glow;
    }

    //method that controls the sequence and speed of when light gets turned on. gets called per frame
    void directionGlowController()
    {
        //first should check if any arrow glows, and if it does it should only increase the timer
        if (anArrowGlows)
        {   
            //if the timer has reached the complete endpoint, we turn off the flag variable and reset the timer
            if (gTimer > stayLitTime + offTime) {
                anArrowGlows = false;
                //UnityEngine.Debug.Log("okay we're turning off the arrow for the next one now");
                gTimer = 0;
                //we then iterate our thing so that the next time an arrow is supposed to glow, it will be the next arrow in the sequence
                sequenceTracker++;
            } else {
                //otherwise the timer keeps going, but...
                gTimer += Time.deltaTime;
                //if we're in our offTime
                if (gTimer > stayLitTime && gTimer < offTime + stayLitTime) {
                    //UnityEngine.Debug.Log("we're in the off phase!!");
                    //...and we haven't already turned off the arrow for offTime, we do so now
                    if (!offSequence)
                    {
                        //UnityEngine.Debug.Log("shutting off arrow now! but now we're in the off phase");
                        showArrow(directionsPattern[sequenceTracker], false);
                    }
                }
            }
        } else {
            //UnityEngine.Debug.Log("Yo we hit this part!");
            //assuming there is no arrow glowing and it's time to put on the next one, we need to check if we've displayed all the lights necessary
            //we check this by seeing if the sequence tracker is at the current round (if we're on round 1, we want to display one arrow, 2 is 2 total arrows, etc...)

            if (sequenceTracker >= currentRound) {
                letPlayerRespond = true;
                //UnityEngine.Debug.Log("okay player, your turn!");

                //we also reset the sequence tracker so that the next time it's time for the computer to run, it's reset

                sequenceTracker = 0;

                //if we're still in the process of displaying arrows and we KNOW that an arrow isn't on right now, we set an arrow on
            } else {
                UnityEngine.Debug.Log("next up, we got the " + sequenceTracker + " spot: " + directionsPattern[sequenceTracker]);
                showArrow(directionsPattern[sequenceTracker], true);
                anArrowGlows = true;
            }
        }
    }

    void playerResponse(dir direction)
    {
        //adds the direction to player pattern
        playerPattern.Add(direction);
        UnityEngine.Debug.Log("Yo you hit the " + direction + "key");

        //resets Shfi's position in case they were moving before:
        player.GetComponent<Transform>().position = new Vector3(0, 0, 0);

        //resets movementVector to ready next one
        MovementVector = new Vector3(0, 0, 0);
        movementStrength = originalMovementStrength;
        switch(direction)
        {
            case (dir.Up):
                MovementVector = Vector3.up;
                break;
            case (dir.Down):
                MovementVector = Vector3.down;
                break;
            case (dir.Left):
                MovementVector = Vector3.left;
                break;
            case (dir.Right):
                MovementVector = Vector3.right;
                break;
        }
        UnityEngine.Debug.Log("Movement Vector is now: " + MovementVector);

        //just to kickstart the movement a bit:
        player.GetComponent<Transform>().position += MovementVector * movementStrength * Time.deltaTime;

        
    }
    
    //compares the arrays
    void compareValues()
    {
        //check through all of player pattern
        for (int i = 0; i < playerPattern.Count; i++)
        {
            //print out how the pattern is looking
            UnityEngine.Debug.Log("The " + i + "th input should be" + directionsPattern[i] + " and you put " + playerPattern[i]);
            //if player pattern doesn't match up, then let the player know
            if (playerPattern[i] != directionsPattern[i])
            {
                // makes it clear you screwed up. In time this is also where the FAIL STATE should occur
                UnityEngine.Debug.Log("Brother you screwed up here, let's clear your playerPattern");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                playerPattern.Clear();
            }
        }
        //the game should check if that was the final round and if it is end the game but if it isn'ts it should clear the list and iterate currentround
        if (playerPattern.Count == directionsPattern.Count)
        {
            UnityEngine.Debug.Log("WOOT! This was the final one!! CongratS!");
            playerPattern.Clear();
            StartCoroutine(switchScene());
        } else
        {
            UnityEngine.Debug.Log("okay good job on that round chief! ready to go again?");
            playerPattern.Clear();
            currentRound++;
        }
    }

    void playerController()
    {
        //based on the given movement, will add the response to playerPattern
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                playerResponse(dir.Up);
            }
            else
            {
                playerResponse(dir.Down);
            }
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                playerResponse(dir.Right);
            }
            else
            {
                playerResponse(dir.Left);
            }
        }
    }

    //this method will get called with every frame.
    void playerMovement()
    {
        //the playerPosition gets modified in this method
        Vector3 playerPosition = player.GetComponent<Transform>().position;
        
        //if the player is currently at the center, nothing should happen
        if (Vector3.Distance(new Vector3(0, 0, 0), playerPosition) <= 0.01)
        {
            //if it's currently moving when this check passes,just reset everything
            if (MovementVector.sqrMagnitude > 0)
            {
                movementStrength = originalMovementStrength;
                playerPosition = new Vector3(0, 0, 0);
                MovementVector = new Vector3(0, 0, 0);

                //when the player has hit the total amount of inputs for the round, only then should the values be compared
                if (playerPattern.Count >= currentRound)
                {
                    //UnityEngine.Debug.Log(direction + "! Okay time for the computer to respond");
                    UnityEngine.Debug.Log("playerPatternCount is " + playerPattern.Count + " and current round is " + currentRound);
                    compareValues();
                    letPlayerRespond = false;
                }
            }

            

        } else
        {
            //if shfi's not currently at the center, we want movementstrength to be affected by gravity
            movementStrength -= gravityStrength * Time.deltaTime;
        }

        //every frame, shfi's position should be affected by movement
        player.GetComponent<Transform>().position += MovementVector * movementStrength * Time.deltaTime;

        //player will always have a vector imposed on them and modulated by time
       // UnityEngine.Debug.Log("player position: " + playerPosition);
    }

    /*
     * pressing issues:
     * - implement a fail state
     *      - this is probably just resetting the scene on fail
     * - implement a visual confirmation of the movement
     *      - have it be a vector * a speed that drops off - a vector in the opposite direction until the player returns to origin
     *      - if a player presses the next key, just reset the position and the applied vector (it will look jumpy but we'll keep it)
     * - implement a transition to next scene on success
     *      - I think there's an easy script for this (just not sure what it'll look like)
     * - implement a way to make it clear when the player can actually input stuff 
     *      - I'm pretty sure I can do this kinda simply by having a similar animation setup on a central shfi character as the arrows, then have him turn on based on player response!
     */

    /*
     * how to have arrow off first, then on
     * i
     */

    /*
     * HOW TO SHOW MOVEMENT
     * when a player presses an accepted button, a vector in that direction gets applied that is continously decelerated by an imposed gravity
     * if player returns to position 0, then we remove every vector applied
     * when another button is pressed, we reset everything (it should look a lil jumpy). It might also be nice to have the arrow light up (something we can achieve by setting isGlowing based on vector
     * 
     */

    IEnumerator switchScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        yield return new WaitForSeconds(1f);
    }
}
