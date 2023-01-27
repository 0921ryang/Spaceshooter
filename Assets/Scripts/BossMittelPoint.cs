using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossMittelPoint : MonoBehaviour
{
    public GameObject shield;
    private float shieldCD;
    // drei Teile, die jeweilig normale Angriff kännen und prüfen, ob BOSS von Player geschossen hat. 
    public Boss B0;
    public Boss B1;
    public Boss B2;
    private float shieldEnd;
    [SerializeField] private GameObject player;// Target für weitere Fähigkeiten...
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shieldCD += Time.deltaTime;
        shieldEnd += Time.deltaTime;
        if (shieldCD > 20)//jede 20 Sekunden benutzt Boss Schild
        {
            Instantiate(shield, transform.position, transform.rotation);
            B0.SetShield(true);
            B1.SetShield(true);
            B2.SetShield(true);
            shieldCD = 0;
        }
        if (shieldEnd >= 25)//es dauert 5 Sekunden
        {
            B0.SetShield(false);
            B1.SetShield(false);
            B2.SetShield(false);
            shieldEnd = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,0.2f);
    }
}
