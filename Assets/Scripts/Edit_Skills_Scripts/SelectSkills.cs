using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkills : MonoBehaviour
{
    public bool Slot_1_Full = false;
    public bool Slot_2_Full = false;
    public bool Slot_3_Full = false;
    public bool Slot_4_Full = false;
    public bool Slot_5_Full = false;
    public bool Slot_6_Full = false;

    public Transform Slot_1;
    public Transform Slot_2;
    public Transform Slot_3;
    public Transform Slot_4;
    public Transform Slot_5;
    public Transform Slot_6;

    public GameObject Api_Button;
 


public void Select_Api()
    {
       if (Slot_1_Full == false) 
       {
            Debug.Log("Work");
            // Moves an object to the set position
            transform.position = new Vector2(-303, 135);
        } 
        if (Slot_2_Full == false)
        {

        }
        if (Slot_2_Full == false)
        {

        }   
        if (Slot_3_Full == false)
        {

        }
        if (Slot_4_Full == false)
        {

        }
        if (Slot_5_Full == false)
        {

        }
        if (Slot_5_Full == false)
        {

        }

    }
}
