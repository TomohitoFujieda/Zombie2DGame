using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    GameObject scoreUI;
    ProgressDirector progressDirector;

    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GameObject.Find("Count");
        progressDirector = GameObject.Find("ProgressDirector").GetComponent<ProgressDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.GetComponent<Text>().text = progressDirector.GetKillCnt().ToString();
    }
}
