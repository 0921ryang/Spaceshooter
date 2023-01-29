using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float sc=1;
    // Start is called before the first frame update
   
    void Start()
    {
        transform.localScale *= sc/4;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
