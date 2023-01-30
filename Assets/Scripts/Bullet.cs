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
    
    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (!other.CompareTag("Player"))
        {
            var collideWith = other.GetComponent<Enemy>();
            Destroy(gameObject);
            if (collideWith != null)
            {
                collideWith.maxSpeed += .2f;
                collideWith.minSpeed += .2f;
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Debug.Log("We hit: " + other.name);
            }
        }
    }
}
