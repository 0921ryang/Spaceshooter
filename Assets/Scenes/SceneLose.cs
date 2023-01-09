using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }
    }
}
