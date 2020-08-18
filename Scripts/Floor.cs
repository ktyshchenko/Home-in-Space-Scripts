using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class Floor : GazeableObject
{
    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        if (Player.instance.activeMode == InputMode.TELEPORT)
        {
            // Set the destination location
            Vector3 destination = hitInformation.point;

            // Set the height of the player - so it doesn't change when they teleport
            destination.y = Player.instance.transform.position.y;

            // Move the player
            Player.instance.transform.position = destination;
        }
    }
}
