using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] private Art bulletArt;

    enum Art
    {
        Boss, Normal
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (bulletArt)
        {
            case Art.Normal:
            {
                float amtToMove = speed * Time.deltaTime;
                transform.Translate(Vector3.down*amtToMove,Space.Self);
                if (transform.position.y < -8f)
                {
                    Destroy(gameObject);
                }
                return;
            }
            case Art.Boss:
            {
                transform.Translate(Vector3.forward*speed*Time.deltaTime);
                if (transform.position.y < -8f)
                {
                    Destroy(gameObject);
                }
                return;
            }
        }
    }
}
