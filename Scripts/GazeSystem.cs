using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class GazeSystem : MonoBehaviour
{
    public GameObject reticle;

    public Color inactiveReticleColor = Color.gray;
    public Color activeReticleColor = Color.red;

    private GazeableObject currentGazeObject;
    private GazeableObject currentSelectedObject;

    private RaycastHit lastHit;

    // Start is called before the first frame update
    private void Start()
    {
        SetReticleColor(inactiveReticleColor);
    }

    // Update is called once per frame
    private void Update()
    {
        UseGaze();
        CheckForInput(lastHit);
    }

    // Make the gaze system
    public void UseGaze()
    {
        Ray raycastRay = new Ray(transform.position, transform.forward);
        RaycastHit hitInformation;

        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 100);

        if (Physics.Raycast(raycastRay, out hitInformation))
        {
            // Check the object's interactability
            GameObject hitObject = hitInformation.collider.gameObject;

            // Get the gazeable object
            GazeableObject gazeObject = hitObject.GetComponentInParent<GazeableObject>();

            if (gazeObject != null)
            {
                // Check if looking at the object for the first time in the frame
                if (gazeObject != currentGazeObject)
                {
                    ClearCurrentObject();
                    currentGazeObject = gazeObject;
                    currentGazeObject.OnGazeEnter(hitInformation);
                    SetReticleColor(activeReticleColor);
                }
                else
                {
                    currentGazeObject.OnGaze(hitInformation);
                }
            }
            else
            {
                ClearCurrentObject();
            }

            lastHit = hitInformation;
        }
        else
        {
            ClearCurrentObject();
        }
    }

    // Set the reticle color
    private void SetReticleColor(Color reticleColor)
    {
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
    }

    private void CheckForInput(RaycastHit hitInformation)
    {
        // Check that the input is pressed ie goes down
        if (Input.GetMouseButtonDown(0) && currentGazeObject != null)
        {
            currentSelectedObject = currentGazeObject;
            currentSelectedObject.OnPress(hitInformation);
        }

        // Check that the button is being held
        else if (Input.GetMouseButton(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnHold(hitInformation);
        }

        // Check that the button is released
        else if (Input.GetMouseButtonUp(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnRelease(hitInformation);
            currentSelectedObject = null;
        }
    }

    // Remove the current gaze object
    private void ClearCurrentObject()
    {
        if (currentGazeObject != null)
        {
            currentGazeObject.OnGazeExit();
            SetReticleColor(inactiveReticleColor);
            currentGazeObject = null;
        }
    }
}
