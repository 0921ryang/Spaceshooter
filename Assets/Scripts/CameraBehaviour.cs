using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject follow;
    [SerializeField] public int delay = 10; // Small delay
    
    public int pointer;
    public Vector3[] positions;
    
    void Start()
    {
        positions = new Vector3[delay];

        for (int i = 0; i < delay; i++)
        {
            positions[i] = follow.transform.position;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = positions[pointer];
        positions[pointer] = follow.transform.position + new Vector3(0, -2, -1);
        
        

        pointer++;
        pointer %= delay;
    }
}