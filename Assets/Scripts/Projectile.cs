using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 50f;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Player3D.velocity;
        _rigidbody.velocity+=transform.forward*speed;
    }

    // Update is called once per frame
    void Update()
    {
        //float amtToMove = speed * Time.deltaTime;
        //transform.Translate(Vector3.forward*amtToMove,Space.Self);
        if (Vector3.Distance(Player3D.trans,transform.position) > 300)
        {
            Destroy(gameObject);
        }
    }
}
