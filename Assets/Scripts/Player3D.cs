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
    private bool flag ;
    private bool stop ;
    private bool stop2 ;
    public static bool boom;

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
        stop = false;
        stop2 = false;
        boom = false;
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
        x = Input.GetAxis("Mouse X")*speed;
        y = Input.GetAxis("Mouse Y")*speed;
        if (x == 0 && y == 0&&!stop)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            stop = true;
        }
        else
        {
            stop = false;
            rb.freezeRotation = false;
            rb.AddRelativeTorque(new Vector3(-y,0,-x));
        }
        //显示UI
        if (!flag)
        {
            ScoreUI.text = "Score: "+score+"\n"+"Lives: " + lives +"\n";
        }
        else if(SceneManager.GetActiveScene().name=="SampleScene")
        {
            ScoreUI.text = "Lives: " + lives + "\n";
            Bounds.color=Color.yellow;
            BossBattle.text = "Go into the sun! There is the root of the problem!";
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
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            if (amtToMove == 0 && vertical == 0&&!stop2)
            {
                rb.velocity=Vector3.zero;
                stop2 = true;
            }
            else if(!(amtToMove == 0 && vertical == 0))
            {
                stop2 = false;
                rb.velocity += transform.TransformDirection(Vector3.up) * vertical;
                rb.velocity += transform.TransformDirection(Vector3.right) * amtToMove;
            }
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -500, 500),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -500, 500)
            );
            if (transform.position.x < -499 || transform.position.x > 499 || transform.position.z < -499 ||
                transform.position.z > 499 ||yRichtung)
            {
                Bounds.color=Color.red;
                Bounds.gameObject.SetActive(true);
            }
            else
            {
                Bounds.color=Color.red;
                Bounds.gameObject.SetActive(false);
            }
                //子弹跟随准星方向发出
                Ray ray = Camera.main.ScreenPointToRay(screen);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                if (Physics.Raycast(ray, out hit)&&hit.transform.tag!="Player"&&hit.transform.tag!="PlayersBullet"&&hit.distance>10)
                {
                    //这里是物体被击中后的代码
                    //hit.point是目标物体的位置
                    if (Input.GetMouseButtonDown(0))
                    {
                        var direction = hit.point - transform.position;
                        var targetRotation = Quaternion.LookRotation(direction);
                        Debug.Log("扫描到对象");
                        GameObject bullet = Instantiate(Bullet, transform.position, targetRotation);
                        Physics.IgnoreCollision(bullet.transform.GetChild(0).GetComponent<Collider>(), GetComponent<Collider>());
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var q = Quaternion.LookRotation(ray.GetPoint(distance)- transform.position, transform.up);
                        GameObject bullet =Instantiate(Bullet, transform.position, q);
                        Physics.IgnoreCollision(bullet.transform.GetChild(0).GetComponent<Collider>(), GetComponent<Collider>());
                    }
                }
                
        }
        
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
        }
        if (score >= 800&&!flag)
        {
            flag = true;
            Instantiate(zuBoss, new Vector3(0, transform.position.y+100, 0), Quaternion.identity);
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }

        if (score == 200)
        {
            StartCoroutine(bombUndestroyable());
            boom = true;
        }
    }

    private IEnumerator bombUndestroyable()
    {
        Bounds.color=Color.red;
        BossBattle.text = "The bomb become indestructible!";
        BossBattle.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        BossBattle.gameObject.SetActive(false);
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
