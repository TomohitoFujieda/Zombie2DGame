using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZomibieController : MonoBehaviour
{
    TurnController turnController;
    MapController mapController;
    CharaController zombieController;
    CharaController playerController;

    // Start is called before the first frame update
    void Start()
    {
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        mapController = GameObject.Find("MapDirector").GetComponent<MapController>();
        zombieController = this.GetComponent<CharaController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharaController>();
        zombieController.SetHp(100);
    }

    // Update is called once per frame
    void Update()
    {

        if (turnController.IsMyTurn(zombieController.GetMyOrder()) && zombieController.walking == false)
        {
            if (zombieController.isDead)
            {
                zombieController.pass = true;
            }
            else
            {
                double deltax = playerController.mapCoordinateX - zombieController.mapCoordinateX;
                double deltay = playerController.mapCoordinateY - zombieController.mapCoordinateY;

                if (Math.Abs(deltax) > 0 || Math.Abs(deltay) > 0)
                {
                    double dirx = deltax / Math.Abs(deltax);
                    double diry = deltay / Math.Abs(deltay);

                    if (Math.Abs(deltax) > Math.Abs(deltay) &&
                        mapController.IsPassableBlk(zombieController.mapCoordinateX+dirx, zombieController.mapCoordinateY))
                    {
                        if (dirx < 0) zombieController.rightUp = true;
                        else zombieController.leftDown = true;
                    }
                    else if (Math.Abs(deltax) < Math.Abs(deltay) &&
                             mapController.IsPassableBlk(zombieController.mapCoordinateX, zombieController.mapCoordinateY+diry))
                    {
                        if (diry < 0) zombieController.leftUp = true;
                        else zombieController.rightDown = true;
                    }
                    else if (mapController.IsPassableBlk(zombieController.mapCoordinateX + dirx, zombieController.mapCoordinateY))
                    {
                        if (dirx < 0) zombieController.rightUp = true;
                        else zombieController.leftDown = true;
                    }
                    else if (mapController.IsPassableBlk(zombieController.mapCoordinateX, zombieController.mapCoordinateY + diry))
                    {
                        if (diry < 0) zombieController.leftUp = true;
                        else zombieController.rightDown = true;
                    }
                    else
                    {
                        zombieController.pass = true;
                    }
                }
            }

        }
    }
}
