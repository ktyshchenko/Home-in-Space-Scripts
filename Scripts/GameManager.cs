using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isStarted = false;
    public static bool isControllerFound = false;
    public static bool isFinished = false;

    private GameObject canvasInstr;
    private GameObject controller;

    public TMP_Text timeText;

    public static float duration = 5.0f;
    private float timeLeft;
    private static float offset = 1.0f; // to make it start at X sec exactly

    // Start is called before the first frame update
    void Start()
    {
        canvasInstr = GameObject.FindGameObjectWithTag("Instructions");
        controller = GameObject.FindGameObjectWithTag("Finish");

        timeLeft = duration + offset;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime; // count time left
        DisplayTime();

        ActivateInstructions();

        ActivateController();
    }

    public void StartGame()
    {
        StartCoroutine(StartingSequence());

        IEnumerator StartingSequence()
        {
            yield return new WaitForSeconds(duration);

            isStarted = true;
            canvasInstr.SetActive(false);
        }
    }

    private void ActivateInstructions()
    {
        if (Player.instance.activeMode == InputMode.INSTR)
        {
            canvasInstr.SetActive(true);
        }
        else if (isStarted)
        {
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

    private void DisplayTime()
    {
        timeText.GetComponent<TextMeshProUGUI>().text = "Game will start in " + (int)timeLeft + " sec";

        if (timeLeft <= -1)
        {
            timeText.GetComponent<TextMeshProUGUI>().text = "Game Started! Press Instructions button to exit";
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
