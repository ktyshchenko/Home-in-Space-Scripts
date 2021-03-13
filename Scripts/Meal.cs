using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class Meal : GazeableButton
{
    public Object prefab;

    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        if (parentPanel.currentActiveButton != null)
        {
            // Player mode to place the food on the table
            Player.instance.activeMode = InputMode.MEAL;

            // Set the prefab to the selected food
            Player.instance.activeMealPrefab = prefab;
        }
    }
}
