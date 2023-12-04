using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static bool boatPuzzle;

    public GameObject NPCbubble1;
    public GameObject NPCbubble2;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (boatPuzzle == true)
        {
            NPCbubble1.SetActive(false);
        }
        else
        {
            NPCbubble2.SetActive(false);
        }
    }
}