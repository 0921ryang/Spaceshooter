using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhole : MonoBehaviour
{
    public float speed = 3f;
    // public int MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float amtToMove = MoveSpeed * Time.deltaTime;
        // transform.Translate(Vector3.forward*amtToMove,Space.Self);
    }

    private void OnTriggerStay(Collider other)
    {
        // if (other != null && !other.CompareTag("BlackHole") &&
        //     Vector3.Distance(other.transform.position, transform.position) > 50f)
        // {
        //     if (other != null && (other.CompareTag("Enemy") || other.CompareTag("Schleim")))
        //     {
        //         other.transform.position =
        //             Vector3.Lerp(other.transform.position, transform.position, 0.05f * Time.deltaTime);
        if (other!=null&&!other.CompareTag("BlackHole")&&Vector3.Distance(other.transform.position, transform.position) > 50f)
        {
            if (other != null && (other.CompareTag("Enemy") || other.CompareTag("Schleim")))
            {
                other.transform.position = Vector3.Lerp(other.transform.position, transform.position, 0.05f * Time.deltaTime);
            }
            else
            {
                Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
                if (otherRigidbody != null)
                {
        //             Vector3 force = (transform.position - other.transform.position).normalized * speed * Time.deltaTime;
        //             otherRigidbody.velocity += force;
        //         }
        //     }

        // }
        // else if (other != null && other.CompareTag("Player"))

                    Vector3 force = (transform.position - other.transform.position).normalized * speed*Time.deltaTime;
                    otherRigidbody.velocity+=force;
                }
            }

        }else if (other!=null&&other.CompareTag("Player"))
        {

        }
        // else if (other != null && !other.CompareTag("BlackHole"))
        // {
        //     other.GetComponent<Renderer>().enabled = false;
        // }
        else if(other!=null&&!other.CompareTag("BlackHole"))
        {
            other.GetComponent<Renderer>().enabled = false;
        } 
    }

    private void OnTriggerExit(Collider other)
    {
    }
}
