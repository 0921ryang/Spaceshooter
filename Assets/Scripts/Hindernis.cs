using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hindernis : MonoBehaviour
{
    private Vector2 maxScale;
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
        po.x += Random.Range(-50f, 50f);
        po.y += Random.Range(20f,50f);
        po.z += Random.Range(-50f, 50f);
        int i = 10;
        var si = GetComponent<SphereCollider>().bounds.extents.magnitude/2;
        while ((po.x<-500||po.x>500||po.z>500||po.z<-500)&&i>0&&Physics.CheckSphere(po, si))
        {
            i--;
            po = Player3D.trans;
            po.x += Random.Range(-100f, 100f);
            po.y += 50;
            po.z += Random.Range(-100f, 100f);
        }

        if (i == 0)
        {
            po = Player3D.trans;
            po.y += 15f;
        }
        transform.position = po;
        //transform.Translate(Random.Range(-4, 4f), 10, 0);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Hindernis[] myScripts = GameObject.FindObjectsOfType<Hindernis>();
        if (other.CompareTag("Player")&&myScripts.Length!=0)
        {
            other.transform.position = myScripts[Random.Range(0,myScripts.Length)].transform.position;
        }
    }
}
