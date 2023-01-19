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
    private Rigidbody playerPosition;//玩家位置
    [SerializeField]
    private float moveSpeed;
    [SerializeField] private int minXPosition, maxXPosition,YPosition;
    [SerializeField] private Art art;
    public GameObject Bullet;
    public int aiLives = 5;
    public enum Art
    {
        Chase, Moving,   
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(minXPosition,maxXPosition),YPosition,0);
        //AI出现的位置，两个XPosition指定一个范围然后随机刷新，然后y轴就自己改了
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public float fireRate = 1f;//射击频率
    private float nextFire = 0.0f;
    private bool toEnd = true;
    private void FixedUpdate()
    {
        switch (art)
        { 
            case Art.Chase:
            {
                transform.Translate(moveSpeed*new Vector3(playerPosition.position.x-transform.position.x,0,
                    playerPosition.position.z-transform.position.z)*Time.deltaTime);
                //上面就是追踪玩家，只对x轴的位置
                if (Time.time>nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Instantiate(Bullet, transform.position, transform.rotation);
                }
                //让子弹射击按设定的时间触发
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
                    toEnd = !toEnd;//反转目标点位
                }
                if (Time.time>nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Instantiate(Bullet, transform.position, transform.rotation);
                }
                //这里就是一个不会追踪，但是会在固定范围内来回移动并且定时射击
                return;
            }
            
        }
    }
    
    public GameObject Effect;//爆炸效果

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
