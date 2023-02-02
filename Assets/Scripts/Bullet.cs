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
            var collideWith1 = other.GetComponent<SchleimEnemy>();
            var collideWith2 = other.GetComponent<Bomb>();
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            if (collideWith != null)
            {
                collideWith.maxSpeed += 7f;
                collideWith.minSpeed += 7f;
            }
            if (collideWith1 != null)
            {
                collideWith.maxSpeed += 10f;
                collideWith.minSpeed += 10f;
            }
            if (collideWith2 != null)
            {
                collideWith.maxSpeed += 5f;
                collideWith.minSpeed += 5f;
            }
        }
    }
}
