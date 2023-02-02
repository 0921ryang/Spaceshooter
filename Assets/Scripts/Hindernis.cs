using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hindernis : MonoBehaviour
{
    private Vector2 maxScale;

    private static int eingehen = 3;
    // Start is called before the first frame update
    void Start()
    {
        SetSpeedAndPosition();
        maxScale.x = 5f;
        maxScale.y = 15f;
        float scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        //transform.rotation=Quaternion.Euler(new Vector3(Random.Range(-90f,90f),Random.Range(-90f,90f),Random.Range(-90f,90f)));
    }
    void SetSpeedAndPosition()
    {
        var po = Player3D.trans;
        po.x += Random.Range(-150f, 150f);
        po.y += Random.Range(20f,50f);
        po.z += Random.Range(-150, 150f);
        int i = 10;
        while ((po.x<-500||po.x>500||po.z>500||po.z<-500)&&i>0)
        {
            i--;
            po = Player3D.trans;
            po.x += Random.Range(-150f, 150f);
            po.y += 50;
            po.z += Random.Range(-150f, 150f);
        }
        
        if (i == 0)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = po;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Hindernis[] myScripts = GameObject.FindObjectsOfType<Hindernis>();
        if ((other.CompareTag("Player")||other.CompareTag("PlayersBullet"))&&myScripts.Length>1)
        {
            eingehen--;
            int ran = Random.Range(0, myScripts.Length);
            if (myScripts[ran] != this)
            {
                other.GetComponent<Rigidbody>().MovePosition(myScripts[ran].transform.position);
            }
            else
            {
                int newRan = (ran + 1) % myScripts.Length;
                if (Physics.CheckSphere(myScripts[newRan].transform.position, 2)&&eingehen>0)
                {
                    other.GetComponent<Rigidbody>().MovePosition(myScripts[newRan].transform.position);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (eingehen == 2)
        {
            eingehen--;
        }else if (eingehen == 0)
        {
            eingehen = 3;
        }
    }
}
