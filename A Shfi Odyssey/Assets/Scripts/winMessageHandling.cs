using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winMessageHandling : MonoBehaviour
{
    public levelChange fader;
    // Start is called before the first frame update
    void Start()
    {
        if (levelHandle.instance.prevScene == 4)
        {
            GameObject.FindWithTag("OperaInstructionsPanel").SetActive(false);
        } else if (levelHandle.instance.prevScene == 5)
        {
            GameObject.FindWithTag("SailorInstructionsPanel").SetActive(false);
        } else
        {
            //turn off the sailor and the opera director instructionspanels
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            fader.FadeToLevel(3);
        }
    }
}
