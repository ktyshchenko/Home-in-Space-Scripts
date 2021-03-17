using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsButton : ModeButton
{
    private Image buttonImg;

    private void Start()
    {
        buttonImg = GetComponent<Image>();
    }

    private void Update()
    {
        ActivateButton();
    }

    private void ActivateButton()
    {
        if (Player.instance.activeMode == InputMode.INSTR)
        {
            buttonImg.enabled = true;
        }
        else
        {
            buttonImg.enabled = false;
        }
    }
}
