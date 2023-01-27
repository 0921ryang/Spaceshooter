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
    public BossHPUI UI;// UI um aktuell HP zu zeigen
    private bool hasShield;//prüfen, ob jetzt Boss Shield hat
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > fireTime)
        {
            Fire();
            currentTime = 0;
        }
        transform.RotateAround(Point.transform.position, rotationAxis, MoveSpeed * Time.deltaTime);
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
            if (UI.getCurrentHP() <= 0)
            {
                Instantiate(Effct, transform.position, transform.rotation);
                Instantiate(Effct, transform.position, transform.rotation);
                Instantiate(Effct, transform.position, transform.rotation);
                Instantiate(Effct, transform.position, transform.rotation);
                Instantiate(Effct, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void SetShield(bool b)
    {
        hasShield = b;
    }
}
