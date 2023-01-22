using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{   
    [SerializeField] private BossMittelPoint Point;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private int MoveSpeed;
    private float currentTime = 0.0f;
    [SerializeField]private float fireTime;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Bullet;
    //Boss  HP UI
    [SerializeField] private Image HP;
    private int bossHP = 76;
    private int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = bossHP;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Point.transform.position, rotationAxis, MoveSpeed * Time.deltaTime);
        //Way of attack（no idea yet）
        currentTime += Time.deltaTime;
        if (currentTime > fireTime)
        {
            Fire();
            currentTime = 0;
        }
        
        if (bossHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        GameObject prefabs = Instantiate(Bullet, transform.position, transform.rotation);
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        prefabs.transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayersBullet"))
        {
            currentHP -= 3;
            ChangeImageSize();
        }
    }

    private void ChangeImageSize()
    {
        HP.rectTransform.sizeDelta = new Vector2(currentHP, 7);
    }
}
