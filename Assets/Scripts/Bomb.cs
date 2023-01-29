using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _speed;
    public GameObject Effect;
    [SerializeField] public float minSpeed = 0.5f;
    [SerializeField] public float maxSpeed = 2f;

    private float maxRotationSpeed = 100f;
    private Vector3 rotationSpeed;

    private Vector2 maxScale;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        maxScale.x = 0.5f;
        maxScale.y = 1f;
        transform.Rotate(Time.deltaTime*rotationSpeed);
        float scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        SetSpeedAndPosition();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(Time.deltaTime*rotationSpeed);
        float amtToMove = _speed * Time.deltaTime;
<<<<<<< Updated upstream
        transform.Translate(- Vector3.up * amtToMove,Space.World);
=======
        if (transform.position.y > Player3D.trans.y)
        {
            transform.Translate(dir*amtToMove,Space.World);
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
>>>>>>> Stashed changes
    }

    void OnBecameInvisible()
    {
        //Debug.Log("yes");
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        Debug.Log("Current lives: "+Player.lives);
        SetSpeedAndPosition();
        float scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
    }

    void SetSpeedAndPosition()
    {
        _speed = Random.Range(minSpeed, maxSpeed);
<<<<<<< Updated upstream
        transform.position=Camera.main.ViewportToWorldPoint( new
            Vector3(Random.Range(0.1f,0.9f), 0.5f, 10));
=======
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
        dir = Player3D.trans;
        dir.x += Random.Range(-20, 20);
        dir.y += Random.Range(-20, 20);
        dir.z += Random.Range(-20, 20);
        dir = (dir - transform.position).normalized;
        Debug.Log(transform.position);
>>>>>>> Stashed changes
        //transform.Translate(Random.Range(-4, 4f), 10, 0);
        
    }
    void OnTriggerEnter(Collider other)
    {
        rotationSpeed.x = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.y = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotationSpeed.z = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        float scale = Random.Range(maxScale.x, maxScale.y);
        transform.localScale = Vector3.one * scale;
        Instantiate(Effect, transform.position, Quaternion.identity);
        SetSpeedAndPosition();
        if (other.CompareTag("Player") || other.CompareTag("PlayersBullet"))
        {
            Player3D.score += 10;
            Debug.Log("You have score"+Player3D.score);
            Debug.Log(other.name);
        }
    }
}
