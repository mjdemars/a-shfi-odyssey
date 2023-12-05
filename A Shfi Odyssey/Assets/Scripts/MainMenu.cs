using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioSource clickSound;

    // Start is called before the first frame update
    public void PlayGame()
    {
        if (!clickSound.isPlaying)
        {
            clickSound.Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
