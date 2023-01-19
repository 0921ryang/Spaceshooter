using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossMittelPoint Point;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private int MoveSpeed;
    private float currentTime = 0.0f;
    [SerializeField]private float fireTime;
    [SerializeField] private GameObject player;//目标
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Point.transform.position, rotationAxis, MoveSpeed * Time.deltaTime); //绕点旋转运动
        currentTime += Time.deltaTime;
        if (currentTime > fireTime)
        {
            currentTime = 0;
            
        }
        
    }

}
