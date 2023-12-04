using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPuzzle : MonoBehaviour
{
    private bool shipPuzzle;

    public GameObject dialoguePanel;

    // Start is called before the first frame update
    void Start()
    {
        shipPuzzle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeSelf)
        {
            if (shipPuzzle == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                shipPuzzle = false;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
    }
}
