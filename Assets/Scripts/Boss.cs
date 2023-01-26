using System;
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
    public GameObject Effct; //explosion effect
    public BossHPUI UI;
    private bool hasShield;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Point.transform.position, rotationAxis, MoveSpeed * Time.deltaTime);
        //Way of attack
        currentTime += Time.deltaTime;
        if (currentTime > fireTime)
        {
            Fire();
            currentTime = 0;
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
        if (other.CompareTag("PlayersBullet") && !hasShield)
        {
            UI.ChangeHP(1);
        }
    }

    public void SetShield(bool b)
    {
        hasShield = b;
    }
}
