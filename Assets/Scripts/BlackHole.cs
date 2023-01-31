using System.Collections;
using System.Collections.Generic;
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
        if (other != null && !other.CompareTag("BlackHole") &&
            Vector3.Distance(other.transform.position, transform.position) > 50f)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Schleim")||other.CompareTag("Explosion"))
            {
                other.transform.position =
                    Vector3.Lerp(other.transform.position, transform.position, 0.05f * Time.deltaTime);
            }
            else
            {
                Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
                if (otherRigidbody != null)
                {
                    Vector3 force = (transform.position - other.transform.position).normalized * speed * Time.deltaTime;
                    otherRigidbody.velocity += force;
                }
            }

        }
        else if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }else if (other.CompareTag("Hindernis"))
        {
            Destroy(other.gameObject);
        }
        else if (!other.CompareTag("BlackHole"))
        {
            other.GetComponent<Renderer>().enabled = false;
        }
    }
}
