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

    public GameObject canvasInstr;
    public GameObject canvasPuzzle2;
    public GameObject canvasPuzzle3;
    public GameObject[] controllerObjs;

    public TMP_Text timeText;
    private AudioSource ac;

    public static float duration = 5.0f;
    private float timeLeft;
    private static float offset = 1.0f; // to make it start at X sec exactly
    public static bool isPlayed = true;

    // Start is called before the first frame update
    void Start()
    {
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
        if (solvedFirst && !solvedSecond)
        {
            PlayAudio();

            canvasPuzzle2.SetActive(true);
        }
        else
        {
            canvasPuzzle2.SetActive(false);
        }

        if (Puzzle2.tableCount == Puzzle2.goal)
        {
            solvedSecond = true;
        }
        else
        {
            solvedSecond = false;
        }
    }

    private void ActivatePuzzle3()
    {
        PlayAudio();

        if (solvedSecond)
        {
            canvasPuzzle3.SetActive(true);
        }
        else
        {
            canvasPuzzle3.SetActive(false);
        }
    }

    public void ActivateController()
    {
        if (isControllerFound)
        {
            PlayAudio();

            foreach (GameObject obj in controllerObjs)
            {
                obj.SetActive(true);
            }
        }
    }

    private void PlayAudio()
    {
        if (!isPlayed)
        {
            ac.PlayOneShot(ac.clip);
            isPlayed = true;
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
