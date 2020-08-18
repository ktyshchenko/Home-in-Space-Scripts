using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public class GazeableObject : MonoBehaviour
{
    public bool isTransformable = false;

    private int objectLayer;
    private const int IGNORE_RAYCAST_LAYER = 2;

    private Vector3 initialPlayerRotation;
    private Vector3 initialObjectRotation;

    private Vector3 initialObjectScale;

    // Start is called before the first frame update
    private void Start()
    {
        // Disable the object's outline after initial set-up
        if (isTransformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;
        }   
    }

    public virtual void OnGazeEnter(RaycastHit hitInformation)
    {
        //Debug.Log("Gaze entered on " + gameObject.name);

        if (isTransformable &&
            Player.instance.activeMode == InputMode.TRANSLATE ||
            Player.instance.activeMode == InputMode.ROTATE ||
            Player.instance.activeMode == InputMode.SCALE)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = true;
        }
    }

    public virtual void OnGaze(RaycastHit hitInformation)
    {
        //Debug.Log("Gaze hold on " + gameObject.name);
    }

    public virtual void OnGazeExit()
    {
        //Debug.Log("Gaze exited on  " + gameObject.name);

        if (isTransformable)
        {
            GetComponentInChildren<cakeslice.Outline>().enabled = false;
        }
    }

    public virtual void OnPress(RaycastHit hitInformation)
    {
        //Debug.Log("Button pressed");

        if (isTransformable)
        {
            // Make sure that when moving the object, the movement is fluid because the object itself is not blocking the ray to the table
            objectLayer = gameObject.layer;
            gameObject.layer = IGNORE_RAYCAST_LAYER;

            // For rotation function - setting initial values
            initialPlayerRotation = Camera.main.transform.rotation.eulerAngles;
            initialObjectRotation = transform.rotation.eulerAngles;

            // Set the initial scale of an object
            initialObjectScale = transform.localScale;
        }
    }

    public virtual void OnHold(RaycastHit hitInformation)
    {
        //Debug.Log("Button held");

        if (isTransformable)
        {
            GazeTransform(hitInformation);
        }
    }

    public virtual void OnRelease(RaycastHit hitInformation)
    {
        //Debug.Log("Button released");

        if (isTransformable)
        {
            gameObject.layer = objectLayer;
        }
    }

    public virtual void GazeTransform(RaycastHit hitInformation)
    {
        // Use the selected transformation function
        switch (Player.instance.activeMode)
        {
            case InputMode.TRANSLATE:
                GazeTranslate(hitInformation);
                break;
            case InputMode.ROTATE:
                GazeRotate(hitInformation);
                break;
            case InputMode.SCALE:
                GazeScale(hitInformation);
                break;
        }
    }

    // Change the position of an object
    public virtual void GazeTranslate(RaycastHit hitInformation)
    {
        if (hitInformation.collider != null && hitInformation.collider.GetComponent<Table>())
        {
            transform.position = hitInformation.point;
        }

    }

    // Rotate an object
    public virtual void GazeRotate(RaycastHit hitInformation)
    {
        float rotationSpeed = 2.0f;

        // Current player's head rotation
        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles; // Euler angles allow to not deal with quaternions (default in Unity for rotation)

        // Current object's rotation
        Vector3 currentObjectRotation = transform.rotation.eulerAngles;

        // Determine how much the head was moved
        Vector3 deltaRotation = currentPlayerRotation - initialPlayerRotation;

        // Calculate and set the rotation for the object on y-xis only
        Vector3 newRotation = new Vector3(currentObjectRotation.x,
            currentObjectRotation.y + (deltaRotation.y * rotationSpeed),
            currentObjectRotation.z);
        transform.rotation = Quaternion.Euler(newRotation); // Need to convert back to quaternion for Unity

    }

    // Change the size of an object
    public virtual void GazeScale(RaycastHit hitInformation)
    {
        float scaleSpeed = 0.1f;
        float scaleFactor = 1;

        // Current player's head rotation
        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles; // Euler angles allow to not deal with quaternions (default in Unity for rotation)

        // Determine how much the head was moved
        Vector3 deltaRotation = currentPlayerRotation - initialPlayerRotation;

        // If looking up - increase the object's size
        if (deltaRotation.x < 0 && deltaRotation.x > -180.0f || deltaRotation.x > 180.0f && deltaRotation.x < 360.0f)
        {
            // Normalize to 180
            if (deltaRotation.x > 180.0f)
            {
                deltaRotation.x = 360.0f - deltaRotation.x;
            }

            scaleFactor = 1.0f + Mathf.Abs(deltaRotation.x) * scaleSpeed;
        }

        // If looking down - reduce the object's size
        else 
        {
            // Normalize to 180
            if (deltaRotation.x < -180.0f)
            {
                deltaRotation.x = 360.0f + deltaRotation.x;
            }

            scaleFactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(deltaRotation.x) * (1.0f / scaleSpeed)) / 180.0f); // Need to have the smalles number can go to - using .Max
        }

        transform.localScale = scaleFactor * initialObjectScale;
    }
}
