using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from https://answers.unity.com/questions/1252922/click-on-object-play-sound-c-help.html

public class RobotSound : GazeableObject
{
    private AudioSource ac;

    // Start is called before the first frame update
    private void Start()
    {
        ac = GetComponent<AudioSource>();
    }

    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        // Play the audio once
        ac.PlayOneShot(ac.clip);
    }
}
