using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public double blkSz;

    public GameObject GetBlockByCoordinate(double x, double y)
    {
        string name = string.Format("{0}_{1}_blk", x, y);
        GameObject retObj = GameObject.Find(name);
        return retObj;
    }

    public bool IsPassableBlk(double x, double y)
    {
        GameObject blk = GameObject.Find(string.Format("{0}_{1}_blk", x, y));
        if (blk == null) return false;
        return blk.GetComponent<BlockController>().passable;
    }

    public GameObject GetPlayerObject()
    {
        GameObject retObj = GameObject.FindWithTag("Player");
        return retObj;
    }


    // Start is called before the first frame update
    void Start()
    {
        blkSz = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
