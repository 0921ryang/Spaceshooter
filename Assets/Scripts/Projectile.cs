using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject playerPos;
    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.forward*amtToMove,Space.Self);
        if (Vector3.Distance(playerPos.transform.position,transform.position) > 150)
        {
            Destroy(gameObject);
        }
    }
}
