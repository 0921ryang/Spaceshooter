using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellit : MonoBehaviour
{
    public bool isLiving;
    public GameObject Effect;
    private int lives = 10;
    // Start is called before the first frame update
    void Start()
    {
        isLiving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayersBullet"))
        {
            lives -= 1;
            Instantiate(Effect,transform.position,transform.rotation);
        }
    }
    
}
