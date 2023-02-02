using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.down*amtToMove,Space.Self);
        if (main == null||Vector3.Distance(main.transform.position, transform.position) > 300)
        {
            Destroy(gameObject);
        }
    }
}
