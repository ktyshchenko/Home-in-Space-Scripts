using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class DragZone : GazeableObject
{
    private VRCanvas parentPanel; // Will be a child of the main camera
    private Transform originalParent; // To reset to the original values afterwards
    private InputMode savedInputMode = InputMode.NONE; // To reset to the original values afterwards

    // Start is called before the first frame update
    private void Start()
    {
        parentPanel = GetComponentInParent<VRCanvas>(); // Get the parent panel
    }

    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        // To move the panel with the camera, make it camera's child
        originalParent = parentPanel.transform.parent;
        parentPanel.transform.parent = Camera.main.transform; // Will move with camera now

        // Set the player's mode
        savedInputMode = Player.instance.activeMode;
        Player.instance.activeMode = InputMode.DRAG;
    }

    public override void OnRelease(RaycastHit hitInformation)
    {
        base.OnRelease(hitInformation);

        // Change back to the initial values
        parentPanel.transform.parent = originalParent;
        Player.instance.activeMode = savedInputMode;
    }
}
