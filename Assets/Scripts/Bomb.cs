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
        transform.Translate(- Vector3.up * amtToMove,Space.World);
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
        transform.position=Camera.main.ViewportToWorldPoint( new
            Vector3(Random.Range(0.1f,0.9f), 0.5f, 10));
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
