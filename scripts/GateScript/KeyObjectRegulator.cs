using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keynetwork
{
public class KeyObjectRegulator : MonoBehaviour
{
         [Header("Sounds  ")]
        public AudioClip objectCompleteSound;
     public AudioSource audioSource;



        [SerializeField] private bool key=false;
        [SerializeField] private bool Gate=false;
        [SerializeField] private KeyList keyList=null;

        private KeyGateRegulator gateObject;

        public void Start()
        {
            gateObject = GetComponent<KeyGateRegulator>();
        }

        public void foundObject()
        {    Debug.Log(key); 
        Debug.Log(Gate); 
        Debug.Log(gameObject); 
            if(key)
            {
                keyList.haskey = true;
                gameObject.SetActive(false);
                 Debug.Log("hutto key."); 
                 audioSource.PlayOneShot(objectCompleteSound);
            }
            else if(Gate)
            {
                gateObject.StatAnimation();
                 
            }
        }
      }
}