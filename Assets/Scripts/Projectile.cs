using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50f;

    public GameObject main;

    // Update is called once per frame
    void Update()
    {
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.forward*amtToMove,Space.Self);
        if (main != null)
        {
            if (Vector3.Distance(main.transform.position,transform.position) > 150)
            {
                Destroy(gameObject);
            }
        }

        if (transform.childCount==0)
        {
            Destroy(gameObject);
        }
    }
}
