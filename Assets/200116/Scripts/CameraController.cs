using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public double mapCoordinateX;
    public double mapCoordinateY;
    double worldX;
    double worldY;
    double blkSz;
    bool focusOnPlayer;
    MapController mapController;
    GameObject player;

    void UpdateCoordinate()
    {
        worldX = this.transform.position.x;
        worldY = this.transform.position.y;
        this.mapCoordinateX = -(worldX + 2 * (worldY - blkSz * (3/4))) / blkSz;
        this.mapCoordinateY = (worldX - 2 * (worldY - blkSz * (3/4))) / blkSz;
        this.gameObject.name = string.Format("{0}{1}_camera", mapCoordinateX, mapCoordinateY);
    }

    IEnumerator TracePlayer()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.worldX = this.transform.position.x;
        this.worldY = this.transform.position.y;
        blkSz = 100;
        this.mapCoordinateX = -(worldX + 2 * (worldY - blkSz * (3/4))) / blkSz;
        this.mapCoordinateY = (worldX - 2 * (worldY - blkSz * (3/4))) / blkSz;
        this.mapController = GameObject.Find("MapDirector").GetComponent<MapController>();
        this.player = mapController.GetPlayerObject();
        focusOnPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (focusOnPlayer)
        {
            StartCoroutine("TracePlayer");
        }
    }
}
