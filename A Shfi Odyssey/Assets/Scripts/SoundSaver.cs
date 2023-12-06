using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSaver : MonoBehaviour
{
    public GameObject sounds;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(sounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
