using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GazeableObject
{
    private Animator anim;
    public bool isOpen = false;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void OnPress(RaycastHit hitInformation)
    {
        base.OnPress(hitInformation);

        // Activate animation transitions
        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
    }
}
