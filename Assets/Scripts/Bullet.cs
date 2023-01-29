using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float speed = 15f;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

<<<<<<< Updated upstream
    void OnTriggerEnter(Collider other)
=======
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }

    void OnCollisionEnter(Collision collision)
>>>>>>> Stashed changes
    {
        var collideWith = other.GetComponent<Enemy>();
        collideWith.maxSpeed += .2f;
        collideWith.minSpeed += .2f;
        Destroy(gameObject);
        if (collideWith != null)
        {
            //explosionPrefab;
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("We hit: " + other.name);
        }
    }
}
