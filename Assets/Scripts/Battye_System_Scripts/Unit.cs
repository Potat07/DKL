using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel;
    public int agilty;
    public float damage;
    public float defense;
    public float maxHP;
    public float currentHP;
    public float magic;
    public int CurrentStamina;
    public int MaxStamina;

    public int ApiWeakness;
    public int EsWeakness;
    public int BatuWeakness;
    public int LogamWeakness;
    public int UdaraWeakness;
    public int Number_of_Health_Tonics;
    public int Number_of_Stamina_Tonics;

    public int Blocking_status;
    // Not blocking = 0
   // Api_Shield = 1
   // Batu_Block = 2
   // Logam_Shield = 3
   // Udara_Shield = 4
   // Es_Block = 5
    public bool TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }


}
