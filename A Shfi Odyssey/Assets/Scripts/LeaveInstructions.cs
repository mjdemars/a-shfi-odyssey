using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveInstructions : MonoBehaviour
{
    private GameObject instructionsPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Underwater puzzle is scene 4; instructions are 6
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
            //otherwise if we're in the opera scene...
            } else if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                instructionsPanel = GameObject.FindWithTag("OperaInstructionsPanel");
                Debug.Log("yoo congrats you found the instructionpanel!!!! wooo!!");
                if (instructionsPanel != null)
                {
                    Debug.Log("WOO!!");
                    instructionsPanel.SetActive(false);
                    //instructionsPanel.activeSelf;
                }
                else
                {
                    Debug.Log("Awww :(");
                }
                
            } else
            {
                //if we're in the win message screen, q should do....nothing?
            }
            
        }
    }
}
