using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject : MonoBehaviour
{
    public float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float amtToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.forward*amtToMove,Space.Self);
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
}
