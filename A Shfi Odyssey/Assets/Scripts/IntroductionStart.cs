using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionStart : MonoBehaviour
{

    public ParticleSystem bubbles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.EmissionModule module = bubbles.emission;
        module.enabled = true;
    }
}
