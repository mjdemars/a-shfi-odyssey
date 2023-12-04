using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPuzzle : MonoBehaviour
{
    public GameObject dialoguePanel;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(player);

        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeSelf)
        {

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
