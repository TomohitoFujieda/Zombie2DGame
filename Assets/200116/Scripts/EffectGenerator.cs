using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerator : MonoBehaviour
{
    public GameObject AttackEffectPrefab;

    public void GenerateEffect(double x, double y)
    {
        Instantiate(AttackEffectPrefab, new Vector2((float)x, (float)y), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
