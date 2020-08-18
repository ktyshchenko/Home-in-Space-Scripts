using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code modified from the edX course Creating Virtual Reality (VR) Apps

public enum InputMode
{
    NONE,
    TELEPORT,
    WALK,
    MEAL,

    TRANSLATE,
    ROTATE,
    SCALE,

    DRAG
}

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField]
    private float speed = 3.0f;

    public InputMode activeMode = InputMode.NONE;

    public GameObject floor;
    public GameObject ceiling;
    public GameObject window;
    public GameObject door;
    public GameObject screen; // use instead of wall because walls are curved
    public GameObject shelf; // use instead of wall because walls are curved
    private int offset = 1;

    public Object activeMealPrefab;

    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(instance.gameObject);
        }

        instance = this; 
    }

    // Update is called once per frame
    private void Update()
    {
        TryWalk();
    }

    public void TryWalk()
    {
        if (Input.GetMouseButton(0) && activeMode == InputMode.WALK)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 newPosition = transform.position + forward * Time.deltaTime * speed;

            if (newPosition.y > floor.transform.position.y+offset && newPosition.y < ceiling.transform.position.y-offset 
               // z is increasing when going towards wall with tv
               // x is increasing when going towards the door
               && newPosition.x > window.transform.position.x && newPosition.x < door.transform.position.x-offset
               && newPosition.z > shelf.transform.position.z+offset && newPosition.z < screen.transform.position.z)
            {
                transform.position = newPosition;
            }
        }
    }
}
