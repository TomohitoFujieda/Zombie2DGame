using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject ZombiePrefab;
    MapController mapController;
    TurnController turnController;
    bool generated;
    
    int turnDelta;

    int leftLimit;
    int rightLimit;
    int topLimit;
    int bottomLimit;

    int randx;
    int randy;

    int blkSz;

    // Start is called before the first frame update
    void Start()
    {
        blkSz = 100;
        mapController = GameObject.Find("MapDirector").GetComponent<MapController>();
        turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
        turnDelta = 5;
        leftLimit = 10;
        rightLimit = 4;
        topLimit = 1;
        bottomLimit = -8;
        generated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnController.GetTurnCount() % turnDelta == 0 && !generated)
        {
            Debug.Log("Generate");
            generated = true;
            randx = Random.Range(rightLimit, leftLimit+1);
            randy = Random.Range(bottomLimit, topLimit+1);

            float worldX = -blkSz/2 * (randx - randy);
            float worldY = -blkSz/4 * (randx + randy) + 75;

            if (mapController.IsPassableBlk(randx, randy))
            {
                Instantiate(ZombiePrefab, new Vector2(worldX, worldY), Quaternion.identity);
            }
        }

        if (turnController.GetTurnCount() % turnDelta == 1) generated = false;
    }
}
