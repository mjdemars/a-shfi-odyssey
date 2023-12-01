using System.Collections;
using System.Collections.Generic;
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

    private int totalRounds = 10;


    List<dir> directionsPattern;
    List<dir> playerPattern; 

    //create a temporary wrong variable that just shines when the value is wrong and then resets
    public SpriteRenderer wrongValue;


    // Start is called before the first frame update
    void Start()
    {
        directionsPattern.Add(dir.Up);
        directionsPattern.Add(dir.Down);
        directionsPattern.Add(dir.Left);
        directionsPattern.Add(dir.Right);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            playerPattern.Add(dir.Up);
            compareValues();
        }
    }
    
    void compareValues()
    {
        for (int i = 0; i < playerPattern.Count; i++)
        {
            if (playerPattern[i] != directionsPattern[i])
            {
                // make it clear you fucked up here
                playerPattern.Clear();

            }
        }
    }
}
