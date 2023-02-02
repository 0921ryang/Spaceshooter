using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteoritBOSS : MonoBehaviour
{
    private float _speed;
    public GameObject Effect;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 2f;

    private float maxRotationSpeed = 100f;
    private Vector3 rotationSpeed;

    private Vector2 maxScale;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        maxScale.x = 3f;
        maxScale.y = 10f;
        transform.Rotate(Time.deltaTime*rotationSpeed);
        float scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        _speed = Random.Range(minSpeed, maxSpeed);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = -Vector3.up * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime*rotationSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.CompareTag("Player") || other.CompareTag("PlayersBullet"))
        {
            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }else if (other.CompareTag("Hindernis"))
        {
            Destroy(gameObject);
        }
    }
}
