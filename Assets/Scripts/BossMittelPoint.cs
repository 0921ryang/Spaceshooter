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
    private GameObject shieldCopy;
    [SerializeField] private GameObject player;// Target für weitere Fähigkeiten...
    //Satellit Setting
    public GameObject Satellit;
    private int count;
    private float check;
    private GameObject tempSatellit;
    //Meteorit
    public GameObject Meteorit;
    private float Min;
    private float Max;
    private float Frequenz;
    //Blackhole 
    //public GameObject BLACKHOLE;
    //private float BlackholeCD;
    //private int COUNT;
    //private int MoveSpeed = 10;
    void Start()
    {
        count = 0;
        Min = transform.position.x - 30;
        Max = transform.position.x + 30;
        //COUNT = 0;

    }

    // Update is called once per frame
    void Update()
    {   
        transform.LookAt(player.transform.position);
        //Schild
        shieldCD += Time.deltaTime;
        if (shieldCD > 20)//jede 20 Sekunden benutzt Boss Schild
        {
            shieldCopy = Instantiate(shield, transform.position, transform.rotation);
            B0.SetShield(true);
            B1.SetShield(true);
            B2.SetShield(true);
            shieldCD = 0;
        }
        if (shieldCopy.IsDestroyed())//es dauert 5 Sekunden
        {
            B0.SetShield(false);
            B1.SetShield(false);
            B2.SetShield(false);
        }
        //Satellit
        if (count==0 && B0.UI.getCurrentHP() <= 150)
        {
            tempSatellit = Instantiate(Satellit,transform.position+new Vector3(15f,-1f,0),transform.rotation);
            count = 1;
        }
        check += Time.deltaTime;
        if (count == 1 && check>=1f)
        {
            B0.UI.Heiling(3);
            check = 0;
        }
        if (tempSatellit.IsDestroyed())
        {
            count = 2;
        }
        // zufällig Meteorit
        Frequenz += Time.deltaTime;
        if (Frequenz > 0.2f)
        {
            Instantiate(Meteorit, new Vector3(Random.Range(Min,Max),player.transform.position.y + 20, player.transform.position.z), transform.rotation);
            Frequenz = 0;
        }
        //black Holes
        /*BlackholeCD += Time.deltaTime;
        if (BlackholeCD >= 3 && COUNT==0)
        {
            Instantiate(BLACKHOLE, transform.position + new Vector3(0, -3, 0), transform.rotation);
            COUNT = 1;
            BlackholeCD = 0;
        }*/
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position,0.2f);
    }
}
