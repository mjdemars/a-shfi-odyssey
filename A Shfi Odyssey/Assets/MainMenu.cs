using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // public ParticleSystem bubbles;

    // Start is called before the first frame update
    public void PlayGame()
    {
        // if (SceneManager.GetActiveScene().buildIndex == 0) {
        //     bubbles.Play();
        // }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
