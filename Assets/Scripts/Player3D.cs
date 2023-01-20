using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player3D : MonoBehaviour
{
    public float speed = 8f;
    private float x;
    private float y;
    public static int lives = 10;
    private Vector3 screen;
    private static float w=Screen.width/2.0f;
    private static float h=Screen.height/2.0f;
    public GameObject Bullet;
    public GameObject explosionPrefab;
    public static int score=0;
    public TMPro.TextMeshPro ScoreUI;

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
    }

    // Update is called once per frame
    void Update()
    {
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
        ScoreUI.text = ScoreUI.text = "Score: "+score+"\n"+"Lives: " + lives +"\n";
        
        if (playerState != State.Explosion)
        {
            //移动
            float amtToMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(Vector3.right*amtToMove);
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(Vector3.up * vertical);
            if (Input.GetMouseButtonDown(0))
            {
                //子弹跟随准星方向发出
                Ray ray = Camera.main.ScreenPointToRay(screen);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);
                if (Physics.Raycast(ray, out hit))
                {
                    //这里是物体被击中后的代码
                    //hit.point是目标物体的位置
                    var direction = hit.point - transform.position;
                    var targetRotation = Quaternion.LookRotation(direction);
                    Debug.Log("扫描到对象");
                    Instantiate(Bullet, transform.position, targetRotation);
                }
                else
                {
                    Instantiate(Bullet, transform.position, Quaternion.LookRotation(transform.up));
                }
            }
        }
        //problematisch
        if (transform.position.x < -10f)
        {
            var transform1 = transform;
            var position = transform1.position;
            position= new Vector3(10f, position.y, position.z);
            transform1.position = position;
        }
        else if (transform.position.x>10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
        }else if (transform.position.y > 5f)
        {
            var transform1 = transform;
            var position = transform1.position;
            position = new Vector3(position.x, -5f, position.z);
            transform1.position = position;
        }
        else if(transform.position.y<-5f)
        {
            transform.position = new Vector3(transform.position.x, 5f, transform.position.z);
        }

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(0);
        }
        if (score >= 200)
        {
            SceneManager.LoadScene(3);
        }
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")&&playerState==State.Playing)
        {
            Instantiate(explosionPrefab, transform.position, other.transform.rotation);
            playerState = State.Explosion;
            StartCoroutine(destroy());
        }
    }

    private IEnumerator destroy()
    {
        GetComponent<Renderer>().enabled = false;
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene(2);
        }
        //problematisch
        transform.position=new Vector3(0,-5,0);
        yield return new WaitForSeconds(3);
        StartCoroutine(blink());
        //Problematisch
        while (transform.position.y<0)
        {
            float vertical1 = speed * Time.deltaTime;
            transform.Translate(Vector3.up * vertical1);
            yield return null;
        }
        playerState = State.Invincible;
        yield return new WaitForSeconds(2);
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
