using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public bool isEndAnim;

    // Start is called before the first frame update
    void Start()
    {
        isEndAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEndAnim)
        {
            Destroy(gameObject);
        }
    }
}
