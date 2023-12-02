using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class OperaGameManager : MonoBehaviour
{
    public SpriteRenderer[] directionSprites;

    private int dirSelect;

    public float stayLit;
    private float stayLitCounter;

    private enum dir
    {
        Up, Down, Left, Right
    }

    public int totalRounds = 10;


    List<dir> directionsPattern = new List<dir>();
    List<dir> playerPattern = new List<dir>();

    //create a temporary wrong variable that just shines when the value is wrong and then resets
    public SpriteRenderer wrongValue;


    // Start is called before the first frame update
    void Start()
    {
        //add the pattern to the callphaseroutine
        generateList();
        showArrow(dir.Up);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                UnityEngine.Debug.Log("Up!");
                playerPattern.Add(dir.Up);
                compareValues();
            } else {
                UnityEngine.Debug.Log("Down!");
                playerPattern.Add(dir.Down);
                compareValues();
            }
        } else if (Input.GetButtonDown("Horizontal")) {
            if (Input.GetAxis("Horizontal") > 0) {
                UnityEngine.Debug.Log("Right");
                playerPattern.Add(dir.Right);
                compareValues(); 
            } else {
                UnityEngine.Debug.Log("Left");
                playerPattern.Add(dir.Left);
                compareValues();
            }
        }
    }

    void addValuesToList()
    {
        directionsPattern.Add(dir.Up);
        directionsPattern.Add(dir.Down);
        directionsPattern.Add(dir.Left);
        directionsPattern.Add(dir.Right);
    }

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
                // make it clear you fucked up here
                UnityEngine.Debug.Log("Brother you screwed up here, let's clear your playerPattern");
                playerPattern.Clear();
            }
        }
        //check if they're done
        if (playerPattern.Count == directionsPattern.Count) {
            UnityEngine.Debug.Log("WOOT! You're done boy, but let's reset in case you want to try again");
            playerPattern.Clear();
        } else
        {
            UnityEngine.Debug.Log("let's keep going");
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

    void showArrow(dir direction)
    {
        //the arrow of the inputted direction will glow and then turn off.
        UnityEngine.Debug.Log(directionSprites.Length);
        directionSprites[(int)direction].color = new Color(1f, 1f, 1f, 1f);
    }

    //roadmap for what to do:
    /*
     1. create a animation for directions
     2. code for the call/response loop
     3. add sprites
     4. 
     */
}
