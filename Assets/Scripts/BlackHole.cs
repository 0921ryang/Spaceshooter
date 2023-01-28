using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other!=null&&!other.CompareTag("BlackHole")&&Vector3.Distance(other.transform.position, transform.position) > 50f)
        {
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                Vector3 force = (transform.position - other.transform.position).normalized * speed*Time.deltaTime;
               otherRigidbody.velocity+=force;
            }

        }else if (other!=null&&other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
        else if(other!=null&&!other.CompareTag("BlackHole"))
        {
            Destroy(other);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
