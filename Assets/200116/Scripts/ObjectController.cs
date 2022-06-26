using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public double mapCoordinateX;
    public double mapCoordinateY;
    double worldX;
    double worldY;
    double blkSz;
    bool firstUpdate;
    SpriteRenderer spriteRenderer;
    GameObject blk;
    MapController mapController;

    // Start is called before the first frame update
    void Start()
    {
        blkSz = 100;
        this.mapController = GameObject.Find("MapDirector").GetComponent<MapController>();
        worldX = this.transform.position.x;
        worldY = this.transform.position.y;
        this.mapCoordinateX = -(worldX + 2 * worldY) / this.blkSz + 1;
        this.mapCoordinateY = (worldX - 2 * worldY) / this.blkSz + 1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Object";
        this.gameObject.name = string.Format("{0}_{1}_obj", mapCoordinateX, mapCoordinateY);
        this.firstUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            blk = mapController.GetBlockByCoordinate(mapCoordinateX, mapCoordinateY);
            blk.GetComponent<BlockController>().passable = false;
        }
    }
}
