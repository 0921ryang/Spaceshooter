using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIenemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerPosition;
    [SerializeField]
    private float moveSpeed;
    [SerializeField] private int minXPosition, maxXPosition,YPosition;
    //Beschränkung des Raums für Enemy
    [SerializeField] private Art art;
    public GameObject Bullet;
    public int aiLives = 5;
    public enum Art
    {
        DistanceKeep, Moving,   
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(minXPosition,maxXPosition),YPosition,0);
        // Enemy wird in diesem Raum beliebig erstellt
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public float fireRate = 1f;//die Frequenz des Angriffs
    private float nextFire = 0.0f;
    private bool toEnd = true;
    private void FixedUpdate()
    {
        switch (art)
        { 
            case Art.DistanceKeep:
            {
                transform.Translate(moveSpeed*new Vector3(playerPosition.position.x-transform.position.x,
                    playerPosition.position.y-transform.position.y+8,
                    playerPosition.position.z-transform.position.z)*Time.deltaTime); 
                //es liegt vor Player und folgt immer den Player und beleibt es immer in y-Achse  mit 8 Einheiten zu Player;
                if (Time.time>nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Instantiate(Bullet, transform.position, transform.rotation);
                }
                //Fire in einer bestimmten Zeit. Man kann es durch die Veränderung von "fireRate" setzen
                return;
            }
            case Art.Moving:
            {
                var nextPosition = toEnd ? new Vector3(maxXPosition,YPosition,0) : new Vector3(minXPosition,YPosition,0);//目标点位
                var amtToMove = (nextPosition - transform.position).normalized;
                amtToMove *= Time.deltaTime * moveSpeed;
                transform.Translate(amtToMove,Space.World);
                if (Vector3.Distance(transform.position, nextPosition) < amtToMove.magnitude)
                {
                    toEnd = !toEnd;
                }
                if (Time.time>nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Instantiate(Bullet, transform.position, transform.rotation);
                }
                //es bewegt immer im bestimmten Bereich(maxXPosition , minXPosition) und schießen
                return;
            }
            
        }
    }
    
    public GameObject Effect;//Explosion Effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayersBullet"))
        {
            aiLives-=1;
            Instantiate(Effect, transform.position, transform.rotation);
        }
        if (aiLives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
