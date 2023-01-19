using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossMittelPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,0.2f);
    }
}
