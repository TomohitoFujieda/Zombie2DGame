using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressDirector : MonoBehaviour
{
    int killCnt;
    bool loaded;
    CharaController player;
    TurnController turnController;

    public void AddKillCnt()
    {
        killCnt++;
    }

    public int GetKillCnt()
    {
        return killCnt;
    }

    void MoveToEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        killCnt = 0;
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharaController>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if ((turnController.GetTurnCount() >= 30 || player.isDead) && !loaded)
        {
            loaded = true;
            MoveToEndScene();
        }
    }
}
