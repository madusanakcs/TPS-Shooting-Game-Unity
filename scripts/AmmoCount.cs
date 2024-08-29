using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammoText;
    public Text MagazineText;

    public static AmmoCount occurence;

    public void Awake()
        {
            occurence=this;
        }


    public void UpdateAmmoText(int presentAmmunition)
    {
        ammoText.text= "Ammo  " + presentAmmunition;


    }


    public void MagText(int mag)
    {
        MagazineText.text="Magazines  "+mag;
    }
    
}
