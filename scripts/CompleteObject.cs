using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteObject : MonoBehaviour
{
    [Header ("Objectives to Complete")]
    public Text Objective1;
    public Text Objective2;
    public Text Objective3;
    public Text Objective4;
    
    public static CompleteObject occurrence;

    public void Awake()
    {
        occurrence=this;
    }


    public void GetObjectivesDone(bool obj1,bool obj2, bool obj3, bool obj4)
    {
        if(obj1==true)
        {
            Objective1.text="1. Key picked up";
            Objective1.color=Color.green;
        }
        else
        {
            Objective1.text="1. Find the key To open The gate";
            Objective1.color=Color.white;
        }

        if(obj2==true)
        {
            Objective2.text="2. Computer is ofline";
            Objective2.color=Color.green;
        }
        else
        {
            Objective2.text="2. Shutdown the computer system";
            Objective2.color=Color.white;
        }

        if(obj3==true)
        {
            Objective3.text="3. Genarator is offline";
            Objective3.color=Color.green;
        }
        else
        {
            Objective3.text="3. Shutdown the both of the genarators";
            Objective3.color=Color.white;
        }
                if(obj4==true)
        {
            Objective4.text="4. Mission Completed";
            Objective4.color=Color.green;
        }
                else
        {
            Objective4.text="4. Find the vehicle and escape from the facility";
            Objective4.color=Color.white;
        }
        
    }
}
