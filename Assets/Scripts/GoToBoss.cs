using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&SceneManager.GetActiveScene().name=="SampleScene")
        {
            SceneManager.LoadScene(4);
        }else if (other.CompareTag("Player")&&SceneManager.GetActiveScene().name=="Boss")
        {
            other.transform.position = new Vector3(0, 0, 0);
        }
    }
}
