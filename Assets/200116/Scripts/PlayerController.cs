using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharaController charaController;
    TurnController turnController;
    bool waitingAttack;
    int power;

    // Start is called before the first frame update
    void Start()
    {
        charaController = GetComponent<CharaController>();
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        charaController.SetHp(1000);
        waitingAttack = false;
        power = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnController.IsMyTurn(charaController.GetMyOrder()))
        {
            if (Input.anyKeyDown == false)
            {
                charaController.keyDown = false;
            }
            else
            {
                charaController.keyDown = true;
            }

            if (waitingAttack)
            {
                if (Input.GetKeyDown(KeyCode.Q) && charaController.walking == false)
                {
                    charaController.Attack(power, charaController.mapCoordinateX, charaController.mapCoordinateY-1);
                }
                else if (Input.GetKeyDown(KeyCode.A) && charaController.walking == false)
                {
                    charaController.Attack(power, charaController.mapCoordinateX+1, charaController.mapCoordinateY);
                }
                else if (Input.GetKeyDown(KeyCode.W) && charaController.walking == false)
                {
                    charaController.Attack(power, charaController.mapCoordinateX-1, charaController.mapCoordinateY);
                }
                else if (Input.GetKeyDown(KeyCode.S) && charaController.walking == false)
                {
                    charaController.Attack(power, charaController.mapCoordinateX, charaController.mapCoordinateY+1);
                }

                else if (Input.GetKeyDown(KeyCode.P) && charaController.walking == false)
                {
                    charaController.pass = true;
                }

                else if (Input.GetKeyDown(KeyCode.F) && charaController.walking == false)
                {
                    waitingAttack = false;
                    Debug.Log("Waiting Move ...");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Q) && charaController.walking == false)
                {
                    charaController.leftUp = true;
                }
                else if (Input.GetKeyDown(KeyCode.A) && charaController.walking == false)
                {
                    charaController.leftDown = true;
                }
                else if (Input.GetKeyDown(KeyCode.W) && charaController.walking == false)
                {
                    charaController.rightUp = true;
                }
                else if (Input.GetKeyDown(KeyCode.S) && charaController.walking == false)
                {
                    charaController.rightDown = true;
                }

                else if (Input.GetKeyDown(KeyCode.P) && charaController.walking == false)
                {
                    charaController.pass = true;
                }

                else if (Input.GetKeyDown(KeyCode.F) && charaController.walking == false)
                {
                    waitingAttack = true;
                    Debug.Log("Waiting Attack ...");
                }
            }
        }
        
    }
}
