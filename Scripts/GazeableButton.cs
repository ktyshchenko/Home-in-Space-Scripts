using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

[RequireComponent(typeof(Image))] // Will enforce that an object has an image component

public class GazeableButton : GazeableObject
{
    protected VRCanvas parentPanel;

    // Start is called before the first frame update
    private void Start()
    {
        parentPanel = GetComponentInParent<VRCanvas>();
    }

    public void SetButtonColor(Color buttonColor)
    {
        GetComponent<Image>().color = buttonColor;
    }

    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        if (parentPanel != null)
        {
            parentPanel.SetActiveButton(this);
        }
    }
}
