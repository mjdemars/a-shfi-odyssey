using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static bool boatPuzzle;

    public GameObject NPCbubble1;
    public GameObject NPCbubble2;

    private GameObject player;
    public Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rBody = player.GetComponent<Rigidbody2D> ();
        rBody.isKinematic = false;
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