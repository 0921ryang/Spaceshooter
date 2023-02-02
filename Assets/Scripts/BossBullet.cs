using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject main;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(main.transform.position, transform.position) > 150)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.CompareTag("Player") || other.CompareTag("Hindernis"))
        {
            Destroy(gameObject);
        }
    }
}
