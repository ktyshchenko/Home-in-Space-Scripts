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
