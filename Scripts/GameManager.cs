using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isStarted = false;
    public static bool isControllerFound = false;
    public static bool isFinished = false;

    private GameObject canvasInstr;
    private GameObject controller;

    // Start is called before the first frame update
    void Start()
    {
        canvasInstr = GameObject.FindGameObjectWithTag("Instructions");
        controller = GameObject.FindGameObjectWithTag("Finish");

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateController();
    }

    public void StartGame()
    {
        StartCoroutine(StartingSequence());

        IEnumerator StartingSequence()
        {
            yield return new WaitForSeconds(60.0f);

            isStarted = true;
            canvasInstr.SetActive(false);
        }
    }

    public void ActivateController()
    {
        if (isControllerFound)
        {
            controller.SetActive(true);
        }
    }

    public void FinishGame()
    {
        if (Door.isOpen && isControllerFound)
        {
            isFinished = true;
        }
    }
}
