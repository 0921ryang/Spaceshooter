using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool hasSatellit;
    private float HeilenCD;
    //hier ist Script für jeden Teil des Boss. Jeder Teil kann schießen, und wenn ein Teil des Boss von Player geschossen wird, nimmt HP ab
    // Aber die besondere Fähigkeit hat ich in Script "BossMittelPoint" geschrieben. 
    void Start()
    {
        hasSatellit = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > fireTime)//fireTime ist Frequenz des Angriff aus BOSS
        {
            Fire();
            currentTime = 0;
        }
        transform.RotateAround(Point.transform.position, rotationAxis, MoveSpeed * Time.deltaTime);
        HeilenCD += Time.deltaTime;
        if (hasSatellit && HeilenCD>=1)
        {
            UI.Heiling(3);
            HeilenCD = 0;
        }
        
    }

    private void Fire()
    {
        transform.LookAt(player.transform.position);
        Instantiate(Bullet, transform.position, transform.rotation).AddComponent<BossBullet>().main = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.CompareTag("PlayersBullet") && !hasShield)
        {
            UI.ChangeHP(1);
            if (UI.getCurrentHP() <= 0)
            {
                Instantiate(Effct, transform.position, transform.rotation);
                Instantiate(Effct, transform.position, transform.rotation);
                Instantiate(Effct, transform.position, transform.rotation);
                Destroy(gameObject);
                SceneManager.LoadScene(3);
            }
        }
    }

    public void SetShield(bool b)
    {
        hasShield = b;
    }

    public void SetSatellit(bool boolean)
    {
        hasSatellit = boolean;
    }
}
