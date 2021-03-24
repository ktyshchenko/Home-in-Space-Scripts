using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
{
    public static int tableCount = 0;
    private bool isOnTable = false;
    public static int goal = 10;

    private void OnCollisionEnter(Collision other) // for 2nd puzzle
    {
        if (other.transform.name == "Table") // food/drink that are transformable count
        {
            isOnTable = true;
            tableCount++;

            //Debug.Log(Table.tableCount);
            //Debug.Log(this.name);

            ActivateAudio();
        }
    }

    private void OnCollisionExit(Collision other) // for 2nd puzzle
    {
        if (other.transform.name == "Table" && isOnTable) // food/drink that are transformable count
        {
            isOnTable = false;
            tableCount--;

            //Debug.Log(Table.tableCount);
            //Debug.Log(this.name);

            ActivateAudio();
        }
    }

    private void ActivateAudio()
    {
        if (tableCount == goal)
        {
            GameManager.isPlayed = false;
        }
    }
}
