using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool isStarted = false;
    public static bool solvedFirst = false;
    public static bool solvedSecond = false;
    public static bool isControllerFound = false; // if third puzzle (math riddle) is solved
    public static bool isFinished = false;

    private GameObject canvasInstr;
    private GameObject canvasPuzzle3;
    public GameObject[] controllerObjs;

    public TMP_Text timeText;
    private AudioSource ac;

    public static float duration = 5.0f;
    private float timeLeft;
    private static float offset = 1.0f; // to make it start at X sec exactly
    private bool isPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        canvasInstr = GameObject.FindGameObjectWithTag("Instructions");
        canvasPuzzle3 = GameObject.FindGameObjectWithTag("Puzzle3");
        ac = GetComponent<AudioSource>();

        timeLeft = duration + offset;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime; // count time left
        DisplayTime();

        ActivateInstructions();

        ActivatePuzzle1();
        ActivatePuzzle2();
        ActivatePuzzle3();

        ActivateController();

        FinishGame();
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

    private void ActivatePuzzle1()
    {
        // TBC
    }

    private void ActivatePuzzle2()
    {
        // TBC
    }

    private void ActivatePuzzle3()
    {
        if (solvedSecond)
        {
            canvasPuzzle3.SetActive(true);
        }
    }

    public void ActivateController()
    {
        if (isControllerFound)
        {
            if (!isPlayed)
            {
                ac.PlayOneShot(ac.clip);
                isPlayed = true;
            }

            foreach (GameObject obj in controllerObjs)
            {
                obj.SetActive(true);
            }
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
