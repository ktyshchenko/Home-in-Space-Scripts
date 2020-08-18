using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class VRCanvas : MonoBehaviour
{
    public GazeableButton currentActiveButton;

    public Color unselectedColor = Color.white;
    public Color selectedColor = Color.green;

    // Update is called once per frame
    private void Update()
    {
        LookAtPlayer();
    }

    public void SetActiveButton(GazeableButton activeButton)
    {
        if (currentActiveButton != null)
        {
            currentActiveButton.SetButtonColor(unselectedColor);
        }

        if (activeButton != null && currentActiveButton != activeButton)
        {
            currentActiveButton = activeButton;
            currentActiveButton.SetButtonColor(selectedColor);
        }
        else
        {
            currentActiveButton = null;
            Player.instance.activeMode = InputMode.NONE;
        }
    }

    // Make the panels face the camera
    public void LookAtPlayer()
    {
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 vectorToPlayer = playerPosition - transform.position; // Destination - origin (panel's position)

        Vector3 lookAtPos = transform.position - vectorToPlayer;

        transform.LookAt(lookAtPos); // Gives a point => face that location
    }
}
