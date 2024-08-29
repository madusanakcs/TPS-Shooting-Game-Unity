using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenarateTurnOff : MonoBehaviour
{
    [Header("Genarator Light and Button")]
    public GameObject greenLight;
    public GameObject redLight;
    public bool button;


    [Header("Genaraor Sound Effects and radious")]
    private float radius_gen =2f;
    public playerScript player;
    public Animator animator;
    public AudioSource audioSource;

       
        [Header("Sounds  ")]
        public AudioClip objectCompleteSound;
 


    private void Awake()
    {
        button =false;
        audioSource=GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown("q") && Vector3.Distance(transform.position ,player.transform.position)<radius_gen)
        {
            button=true;
            animator.enabled=false;
            greenLight.SetActive(false);
            redLight.SetActive(true);
            audioSource.Stop();

            // Object complete
             CompleteObject.occurrence.GetObjectivesDone(true,true,true,false);
                            audioSource.PlayOneShot(objectCompleteSound);
        }
    }
}
