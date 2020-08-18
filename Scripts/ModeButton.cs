using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class ModeButton : GazeableButton
{
    [SerializeField] // Makes the private variable visible in the editor
    private InputMode mode;

    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        if (parentPanel.currentActiveButton != null)
        {
            Player.instance.activeMode = mode;
        }
    }
}
