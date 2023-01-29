using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPUI : MonoBehaviour
{
    public int maxHP = 300;
    private int currentHP;
    public BossHPSet bossHPset;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        bossHPset.SetMax(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeHP(int damage)
    {
        if (currentHP > 0)
        {
            currentHP -= damage;
        }
        bossHPset.SetHP(currentHP);
    }

    public void Heiling(int Zahl)
    {
        if (currentHP > 0 && currentHP <= maxHP)
        {
            currentHP += Zahl;
        }
        bossHPset.SetHP(currentHP);
    }
    public int getCurrentHP()
    {
        return currentHP;
    }
}
