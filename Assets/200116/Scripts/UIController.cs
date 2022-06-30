using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    bool isWalk; 
    GameObject attackToggle;
    GameObject walkToggle;
    GameObject hpBar;
    GameObject remainTurn;
    GameObject player;
    TurnController turnController;
    GameObject rightUpArrow;
    GameObject rightUpArrowFont;
    GameObject leftUpArrow;
    GameObject leftUpArrowFont;
    GameObject rightDownArrow;
    GameObject rightDownArrowFont;
    GameObject leftDownArrow;
    GameObject leftDownArrowFont;
    GameObject passWindow;
    GameObject passText;

    public void ActivateUIArrow(int dirx, int diry)
    {
        if (dirx == 0 && diry == 1)
        {
            rightDownArrow.GetComponent<RawImage>().color = new Color32(255, 255, 0, 255);
            rightDownArrowFont.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
        }
        else if (dirx == 0 && diry == -1)
        {
            leftUpArrow.GetComponent<RawImage>().color = new Color32(255, 255, 0, 255);
            leftUpArrowFont.GetComponent <Text>().color = new Color32(255, 255, 0, 255);
        }
        else if (dirx == 1 && diry == 0)
        {
            leftDownArrow.GetComponent <RawImage>().color = new Color32(255, 255, 0, 255);
            leftDownArrowFont.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
        }
        else if (dirx == -1 && diry == 0)
        {
            rightUpArrow.GetComponent<RawImage>().color = new Color32(255, 255, 0, 255);
            rightUpArrowFont.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
        }
    }

    public void InactivateUIArrow()
    {
        rightDownArrow.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        rightDownArrowFont.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        leftUpArrow.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        leftUpArrowFont.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        leftDownArrow.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        leftDownArrowFont.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        rightUpArrow.GetComponent<RawImage>().color = new Color32(255, 255, 255, 255);
        rightUpArrowFont.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
    }

    public void ActivatePassUI()
    {
        passWindow.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        passText.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
    }

    public void InActivatePassUI()
    {
        passWindow.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        passText.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
    }

    public void ToggleActionUI()
    {
        if (isWalk)
        {
            attackToggle.SetActive(true);
            walkToggle.SetActive(false);
            isWalk = false;
        }
        else
        {
            attackToggle.SetActive(false);
            walkToggle.SetActive(true);
            isWalk = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        this.hpBar = GameObject.Find("HPBarRemain");
        this.remainTurn = GameObject.Find("RemainTurn");
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        rightUpArrow = GameObject.Find("ArrowW");
        rightUpArrowFont = GameObject.Find("W");
        leftUpArrow = GameObject.Find("ArrowQ");
        leftUpArrowFont = GameObject.Find("Q");
        rightDownArrow = GameObject.Find("ArrowS");
        rightDownArrowFont = GameObject.Find("S");
        leftDownArrow = GameObject.Find("ArrowA");
        leftDownArrowFont = GameObject.Find("A");
        attackToggle = GameObject.Find("AttackToggle");
        walkToggle = GameObject.Find("WalkToggle");
        passWindow = GameObject.Find("PassWindow");
        passText = GameObject.Find("PassText");

        attackToggle.SetActive(false);
        walkToggle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float posX = (float)player.GetComponent<CharaController>().hp/4.0f + 1340 - 960;
        float widthX = (float)player.GetComponent<CharaController>().hp/2.0f;

        hpBar.GetComponent<RectTransform>().localPosition = new Vector3(
            posX,
            -450,
            1);
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(
            widthX,
            50
            );
        remainTurn.GetComponent<Text>().text = (30 - turnController.GetTurnCount()).ToString();
    }
}
