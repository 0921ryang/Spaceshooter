using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   private float _speed;
    public GameObject Effect;
    [SerializeField] public float minSpeed = 10f;
    [SerializeField] public float maxSpeed = 30f;

    private float maxRotationSpeed = 100f;
    private Vector3 rotationSpeed;
    private float scale;

    private Vector2 maxScale;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        maxScale.x = 5f;
        maxScale.y = 10f;
        transform.Rotate(Time.deltaTime*rotationSpeed);
        scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        SetSpeedAndPosition();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime*rotationSpeed);
        float amtToMove = _speed * Time.deltaTime;
        if (transform.position.y > Player3D.trans.y)
        {
            transform.Translate(-Vector3.up*amtToMove,Space.World);
        }
        else if(transform.position.y<Player3D.trans.y-130)
        {
            rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            Debug.Log("Current lives: "+Player.lives);
            SetSpeedAndPosition();
            scale = Random.Range(maxScale.x, maxScale.y);
            transform.localScale = Vector3.one * scale;
        }
    }

    void OnBecameInvisible()
    {
        GetComponent<Renderer>().enabled = true;
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
            Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.7f), Random.Range(0.3f, 0.7f), 100f));
        int i = 10;
        var si = GetComponent<SphereCollider>().bounds.extents.magnitude;
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
        Debug.Log(transform.position);
        //transform.Translate(Random.Range(-4, 4f), 10, 0);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        //Collider other = collision.collider;
        if (!(other.GetComponent<SphereCollider>()!=null&&other.CompareTag("BlackHole")))
        {
            Debug.Log(other.name);
            rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            scale = Random.Range(maxScale.x, maxScale.y);
            transform.localScale = Vector3.one * scale;
            Instantiate(Effect, transform.position, Quaternion.identity).GetComponent<Effect>().sc=scale;
            SetSpeedAndPosition();
            if (other.CompareTag("Player") || other.CompareTag("PlayersBullet"))
            {
                Player3D.score += 10;
                Debug.Log("You have score"+Player3D.score);
                Debug.Log(other.name);
            }
        }
    }
}
