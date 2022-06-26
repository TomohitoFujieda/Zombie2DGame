using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public double mapCoordinateX;
    public double mapCoordinateY;
    double worldX;
    double worldY;
    double blkSz;
    public bool passable;
    SpriteRenderer spriteRenderer;
    MapController mapController;

    // Start is called before the first frame update
    void Start()
    {
        blkSz = 100;
        this.mapController = GameObject.Find("MapDirector").GetComponent<MapController>();
        worldX = this.transform.position.x;
        worldY = this.transform.position.y;
        this.mapCoordinateX = -(worldX + 2 * worldY) / this.blkSz;
        this.mapCoordinateY = (worldX - 2 * worldY) / this.blkSz;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Field";
        this.gameObject.name = string.Format("{0}_{1}_blk", mapCoordinateX, mapCoordinateY);
        passable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
