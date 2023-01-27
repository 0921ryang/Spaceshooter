using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHole : MonoBehaviour
{
    public float speed = 0.05f;
    Coroutine moveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        moveCoroutine=StartCoroutine(MoveTowards(other));
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(moveCoroutine);
    }

    IEnumerator MoveTowards(Collider other)
    {
        while (other!=null&&Vector3.Distance(other.transform.position, transform.position) > 50f)
        {
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, speed );
            yield return new WaitForEndOfFrame();
        }

        if (other!=null&&other.CompareTag("Player"))
        {
            SceneManager.LoadScene(2);
        }
        else if(other!=null)
        {
            Destroy(other);
        }
    }
}
