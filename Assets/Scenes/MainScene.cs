using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.anyKey)
        {
            Player3D.lives = 10;
            Player3D.score = 0;
            SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            Player3D.lives = 10;
            Player3D.score = 0;
            SceneManager.LoadScene(1);
        }
    }
}
