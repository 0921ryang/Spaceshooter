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
        if (other.CompareTag("Enemy"))
        {
            var collideWith = other.GetComponent<Enemy>();
            var collideWith1 = other.GetComponent<Bomb>();
            if (collideWith == null)
            {
                collideWith1.maxSpeed += 0.1f;
                collideWith1.minSpeed += 0.1f;
            }
            else
            {
                collideWith.maxSpeed += 0.1f;
                collideWith.minSpeed += 0.1f;
            }
        }else if (other.CompareTag("Schleim"))
        {
            var collideWith = other.GetComponent<SchleimEnemy>();
            collideWith.maxSpeed += .2f;
            collideWith.minSpeed += .2f;
        }
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("We hit: " + other.name);
        }
    }
}
