using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchleimEnemy : MonoBehaviour
{
       private float _speed;
    public GameObject Effect;

    private Vector2 maxScale;
    private float scale;

    private Rigidbody enemyRigidbody;

    private float chaseSpeed=10;
    // Start is called before the first frame update
    void Start()
    {
        maxScale.x = 3f;
        maxScale.y = 10f;
        scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        enemyRigidbody = GetComponent<Rigidbody>();
        SetSpeedAndPosition();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(Player3D.trans);
        Intercept(Player3D.trans,Player3D.velocity);
    }
    private void Chase(Vector3 targetPosition, float speed)
    {
        enemyRigidbody.velocity = (targetPosition - transform.position).normalized * speed;
    }

    void Intercept(Vector3 targetPosition,Vector3 targetVelocity)
    {
        var relativ = targetVelocity - enemyRigidbody.velocity;
        var distance = Vector3.Distance(targetPosition,transform.position);
        var timeToClose = distance / relativ.magnitude;
        var point = targetPosition + timeToClose * targetVelocity;
        Chase(point,chaseSpeed);
    }
    void OnBecameInvisible()
    {
        GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("Current lives: "+Player.lives);
        SetSpeedAndPosition();
        scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
    }

    void SetSpeedAndPosition()
    {
        var po =
            Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 100f));
        int i = 10;
        var si = GetComponent<CapsuleCollider>().bounds.extents.magnitude/2;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag("Player")||other.CompareTag("PlayersBullet"))
        {
            scale = Random.Range(maxScale.x, maxScale.y);
            transform.localScale = Vector3.one * scale;
            Instantiate(Effect, transform.position, Quaternion.identity).transform.localScale=transform.localScale/8;
            SetSpeedAndPosition();
            Player3D.score += 10;
        }
    }
}
