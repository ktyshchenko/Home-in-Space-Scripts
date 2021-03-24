using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle3 : GazeableButton
{
    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        if (this.name == "Number (1)")
        {
            this.SetButtonColor(Color.green);
            GameManager.solvedThird = true;
            GameManager.isPlayed = false;
        }
        else
        {
            StartCoroutine(WrongColor());
        }
    }

    IEnumerator WrongColor()
    {
        this.SetButtonColor(Color.red);

        yield return new WaitForSeconds(0.5f);

        this.SetButtonColor(Color.white);
    }
}
