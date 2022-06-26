using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public bool moveToNext;
    int turnCount;
    int charCount;
    int actionCount;

    public int GetTurnCount()
    {
        return turnCount;
    }

    public int AddCharactor()
    {
        return charCount++;
    }

    public bool IsMyTurn(int n)
    {
        return actionCount == n;
    }

    // Start is called before the first frame update
    void Start()
    {
        turnCount = 1;
        actionCount = 0;
        charCount = 0;
        moveToNext = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToNext)
        {
            Debug.Log("TurnEnd");
            actionCount++;
            if (actionCount >= charCount)
            {
                turnCount++; actionCount = 0;
            }
            moveToNext = false;
        }
    }
}
