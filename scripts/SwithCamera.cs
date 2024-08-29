using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwithCamera : MonoBehaviour
{   

    [Header("Camera to Assign")]
    public GameObject AimCam;
    public GameObject AimCanvas  ;
    public GameObject ThirdPersonCam  ;
    public GameObject ThirdPersonCanvas  ;

    public PlayableDirector Timeline;


    [Header("Camera Animator")]
    public Animator animator;







    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2")  &&   Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
        {
            animator.SetBool("idle", false);
            animator.SetBool("idleAim", true);
            animator.SetBool("aimWalk", true);
            animator.SetBool("walk", true);





            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

            
            
            }
        // timeline is playing canvas is disabled
        else if(Timeline.state == PlayState.Playing)
        {
            ThirdPersonCanvas.SetActive(false);
            AimCanvas.SetActive(false);
        }
      
        
        else if(Input.GetButton("Fire2") )
        {   

            animator.SetBool("idle", false);
            animator.SetBool("idleAim", true);
            animator.SetBool("aimWalk", false);
            animator.SetBool("walk", false);
            
            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }
    
        else{

            animator.SetBool("idle", true);
            animator.SetBool("idleAim", false);
            animator.SetBool("aimWalk", false);



            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);


        }
    
    }
}
