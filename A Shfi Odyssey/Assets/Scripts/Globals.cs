using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{

    public static bool boatPuzzle;
    public static bool operaPuzzle;

    public GameObject NPCbubble1;
    public GameObject NPCbubble2;

    public GameObject SageFish;
    public GameObject SageFishBubble;

    private GameObject player;
    private Rigidbody2D rBody;

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
            NPCbubble2.SetActive(true);
        }
        else 
        {
            NPCbubble2.SetActive(false);
        }

        if (operaPuzzle == true)
        {
            SageFish.SetActive(true);
            SageFishBubble.SetActive(true);
            NPCbubble2.SetActive(false);
        }
    }
}