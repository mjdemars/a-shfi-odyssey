using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPuzzle : MonoBehaviour
{
    public GameObject dialoguePanel;

    private GameObject player;

    public Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rBody = player.GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeSelf)
        {
            rBody.isKinematic = true;
            DontDestroyOnLoad(player);


            if (Globals.boatPuzzle == false)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Globals.boatPuzzle = true;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
    }
}
