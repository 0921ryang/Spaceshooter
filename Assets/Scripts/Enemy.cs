using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//using Random;

public class Enemy : MonoBehaviour {

    private float _speed;
    public GameObject Effect;
    [SerializeField] public float minSpeed = 10f;
    [SerializeField] public float maxSpeed = 30f;

    private float maxRotationSpeed = 100f;
    private Vector3 rotationSpeed;

    private Vector2 maxScale;
    private float scale;

    private Vector3 dir;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        maxScale.x = 30f;
        maxScale.y = 50f;
        transform.Rotate(Time.fixedDeltaTime*rotationSpeed);
        scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        _rigidbody = GetComponent<Rigidbody>();
        SetSpeedAndPosition();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Time.fixedDeltaTime*rotationSpeed);
    }

    void OnBecameInvisible()
    {
        GetComponent<MeshRenderer>().enabled = true;
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        Debug.Log("Current lives: "+Player.lives);
        SetSpeedAndPosition();
        scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
    }

    void SetSpeedAndPosition()
    {
        _speed = Random.Range(minSpeed, maxSpeed);
        var po =
            Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 100f));
        int i = 10;
        var si = GetComponent<BoxCollider>().size.magnitude/2;
        while ((po.x<-500||po.x>500||po.z>500||po.z<-500)&&i>0&&Physics.CheckSphere(po, si))
        {
            i--;
            po=Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 100f));
        }

        if (i == 0)
        {
            po = Player3D.trans;
            po.y += 15f;
        }
        transform.position = po;
        dir = Player3D.trans;
        dir.x += Random.Range(-20, 20);
        dir.y += Random.Range(-20, 20);
        dir.z += Random.Range(-20, 20);
        dir = (dir - transform.position).normalized;
        if (Random.Range(0, 7) <= 4)
        {
            dir = -Player3D.ray.direction;
        }

        _rigidbody.velocity = dir * _speed;
        //transform.Translate(Random.Range(-4, 4f), 10, 0);

    }
    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag("Player")||other.CompareTag("PlayersBullet"))
        {
            rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            scale = Random.Range(maxScale.x, maxScale.y);
            transform.localScale = Vector3.one * scale;
            Instantiate(Effect, transform.position, Quaternion.identity).transform.localScale=transform.localScale/16;
            SetSpeedAndPosition();
            Player3D.score += 10;
            Debug.Log("You have score"+Player3D.score);
            Debug.Log(other.name);
        }
    }
}


