using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class Table : GazeableObject
{
    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        if (Player.instance.activeMode == InputMode.MEAL)
        {
            // Create the meal
            GameObject placedMeal = GameObject.Instantiate(Player.instance.activeMealPrefab) as GameObject;

            // Set its position
            placedMeal.transform.position = hitInformation.point;
        }
    }
}
