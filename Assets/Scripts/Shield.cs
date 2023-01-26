using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float CD;
    
    // Start is called before the first frame update
    void Start()
    {
        CD = 5;
    }

    // Update is called once per frame
    void Update()
    {
        CD -= Time.deltaTime;
        if (CD <= 0)
        {
            Destroy(gameObject);
        }
    }
}
