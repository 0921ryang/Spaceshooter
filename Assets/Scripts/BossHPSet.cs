using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPSet : MonoBehaviour
{
    //hier ist nur eine Hilfsklasse, um besser HP UI zu kontrollieren
    public Slider slider;

    public void SetMax(int hp)
    {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void SetHP(int hp)
    {
        slider.value = hp;
    }
}
