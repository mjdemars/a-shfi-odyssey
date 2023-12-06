using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GameObject backgroundSound;

    // Start is called before the first frame update
    public void PlayGame()
    {
        backgroundSound = GameObject.FindGameObjectWithTag("IntroSounds");
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (backgroundSound) Destroy(backgroundSound);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
