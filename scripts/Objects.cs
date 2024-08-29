using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public float objectHealth =1000f;
    public void objectHitDamage(float amount){

        objectHealth -=amount;

        if(objectHealth<=0f)
        {
            // destory
            Die();

        }


    }


    void Die()
    {

       
        Destroy(gameObject);

    }
}
