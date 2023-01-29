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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
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
