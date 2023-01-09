using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player3D : MonoBehaviour
{
    public float speed = 8f;
    
    public static int lives = 10;
    public static int miss = 0;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        var direction = (mousePos-transform.position).normalized;
        Debug.Log(mousePos);
        var rotation = Quaternion.LookRotation(direction);
        ScoreUI.text = ScoreUI.text = "Score: "+score+"\n"+"Lives: " + lives +"\n"+ "Missing: "+miss+"\n";;
        //transform.Translate(Vector3.right * 0.01f);这个句子会添加一个一直往右的速度
        if (playerState != State.Explosion)
        {
            float amtToMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(Vector3.right*amtToMove);
            float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Translate(Vector3.up * vertical);
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(Bullet, transform.position, rotation);
            }
        }
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
        transform.position=new Vector3(0,-5,0);
        yield return new WaitForSeconds(3);
        StartCoroutine(blink());
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
