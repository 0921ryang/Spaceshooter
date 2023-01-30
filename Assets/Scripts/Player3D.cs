using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player3D : MonoBehaviour
{
   public float speed = 8f;
    private float x;
    private float y;
    public static int lives = 10;
    public static float distance = 15;
    private Vector3 screen;
    private static float w=Screen.width/2.0f;
    private static float h=Screen.height/2.0f;
    public Image image;
    public GameObject Bullet;
    public GameObject explosionPrefab;
    public static int score=0;
    public TMPro.TMP_Text ScoreUI;
    public TMPro.TMP_Text Respawn;
    public TMPro.TMP_Text Bounds;
    public TMPro.TMP_Text BossBattle;
    private Rigidbody rb;
    public static Vector3 trans;
    private static bool yRichtung;
    public GameObject zuBoss;
    private bool flag = false;

    public enum State
    {
        Playing,
        Explosion,
        Invincible
    };
    private State playerState=State.Playing;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        screen = new Vector3(w, h, 0.0f);
        image.enabled = false;
        Respawn.gameObject.SetActive(false);
        Bounds.gameObject.SetActive(false);
        BossBattle.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        trans = transform.position;
        yRichtung = false;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        trans = transform.position;
        //防止屏幕中途变换
        w=Screen.width/2.0f; 
        h=Screen.height/2.0f;
        screen = new Vector3(w, h, 0.0f);
        //鼠标消失
        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
        //三维旋转
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(-y,0,-x),Space.Self);
        //显示UI
        if (!flag)
        {
            ScoreUI.text = "Score: "+score+"\n"+"Lives: " + lives +"\n";
        }
        else if(SceneManager.GetActiveScene().name=="SampleScene")
        {
            ScoreUI.text = "Lives: " + lives + "\n";
            BossBattle.gameObject.SetActive(true);
        }
        else
        {
            ScoreUI.text = "Lives: " + lives + "\n";
            BossBattle.gameObject.SetActive(false);
        }
        
        
        if (playerState != State.Explosion)
        {
            //移动
            float amtToMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            rb.velocity += Vector3.up*amtToMove;
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            rb.velocity += Vector3.up*vertical;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -500, 500),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -500, 500)
            );
            if (transform.position.x < -499 || transform.position.x > 499 || transform.position.z < -499 ||
                transform.position.z > 499 ||yRichtung)
            {
                Bounds.gameObject.SetActive(true);
            }
            else
            {
                Bounds.gameObject.SetActive(false);
            }
                //子弹跟随准星方向发出
                Ray ray = Camera.main.ScreenPointToRay(screen);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                if (Physics.Raycast(ray, out hit)&&hit.transform.tag!="Player"&&hit.transform.tag!="PlayersBullet")
                {
                    //这里是物体被击中后的代码
                    //hit.point是目标物体的位置
                    if (Input.GetMouseButtonDown(0))
                    {
                        var direction = hit.point - transform.position;
                        var targetRotation = Quaternion.LookRotation(direction);
                        Debug.Log("扫描到对象");
                        Instantiate(Bullet, transform.position, targetRotation);
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var q = Quaternion.LookRotation(ray.GetPoint(distance)- transform.position, transform.up);
                        Instantiate(Bullet, transform.position, q);
                    }
                }
                
        }
        
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
        }
        if (score >= 500&&!flag)
        {
            flag = true;
            Instantiate(zuBoss, new Vector3(0, transform.position.y+100, 0), Quaternion.identity);
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        if ((other.CompareTag("Enemy")||other.CompareTag("Explosion"))&&playerState==State.Playing)
        {
            if(other.CompareTag("Enemy"))
            {
                lives--;
            }else
            {
                lives -= 3;
            }
            Instantiate(explosionPrefab, transform.position, other.transform.rotation);
            playerState = State.Explosion;
            StartCoroutine(destroy());
        }else if (other.CompareTag("Schleim"))
        {
            StartCoroutine(Slow());
        }else if (other.CompareTag("Edge"))
        {
            Bounds.gameObject.SetActive(true);
            yRichtung = true;
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        Collider other = collision.collider;
        if (other.CompareTag("Edge"))
        {
            Bounds.gameObject.SetActive(false);
            yRichtung = false;
        }
    }

    private IEnumerator Slow()
    {
        speed = 2;
        StartCoroutine(Mud());
        yield return new WaitForSeconds(10);
        speed = 8;
    }

    private IEnumerator Mud()
    {
        image.enabled = true;
        yield return new WaitForSeconds(1);
        image.enabled = false;
    }
    private IEnumerator destroy()
    {
        GetComponent<Renderer>().enabled = false;
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
        Respawn.color=Color.red;
        Respawn.text = "respawning!" ;
        Respawn.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        Respawn.color=Color.green;
        Respawn.text = "respawned!";
        Respawn.gameObject.SetActive(true);
        StartCoroutine(blink());
        playerState = State.Invincible;
        yield return new WaitForSeconds(2);
        Respawn.gameObject.SetActive(false);
        playerState = State.Playing;
    }

    private IEnumerator blink()
    {
        while (playerState!=State.Playing)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
