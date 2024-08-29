using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Header("Computer On / Off")]
    public bool  lightsOn=true;
    private  float radius_com=5f;
    public Light light;

    [Header("Compter Assign Things")]
    public playerScript player;
    [SerializeField] private GameObject ComputerUI;
    [SerializeField] private int showComputerUIfor=5;
       
       
        [Header("Sounds  ")]
        public AudioClip objectCompleteSound;
 
     public AudioSource audioSource;


    private void Awake()
    {
        light=GetComponent<Light>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position,player.transform.position)<radius_com)
        {
            Debug.Log("Hii pako");
            if(Input.GetKeyDown("q"))
            {  
                StartCoroutine(ShowComputerUI1());
                lightsOn=false;
                light.intensity=0;
                Debug.Log("computer off.");
                // objective  complete
               CompleteObject.occurrence.GetObjectivesDone(true,true,false,false);
               audioSource.PlayOneShot(objectCompleteSound);
            }
        }
    }
    IEnumerator ShowComputerUI1(){
        ComputerUI.SetActive(true);
         yield return new WaitForSeconds(showComputerUIfor); 
        ComputerUI.SetActive(false);
    }
}
