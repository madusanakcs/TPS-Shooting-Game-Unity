using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerScript : MonoBehaviour
{   
    [Header("player Movement")]
    public float playerSpeed = 1.9f;
    public float playersprint =3f;
     public float playerCrouch =1.5f;

    [Header("Player animator and Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;


    [Header("Player Script Camara")]
    public Transform playerCamera;
    public GameObject deathCam;
    public GameObject EndGameMenuUI;

     [Header("Player Health Things")]
     private float playerHealth=120f;
     private float presentHealth;
     public HealthBar healthBar1;
         public AudioClip playerHurtSound;
     public AudioSource audioSource;


    [Header("Player Jumping and Velocity")]
    public float turncalmTime =0.1f;
    float turncalmVelocity ;

    public float jumpRange=1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance=0.4f;
    public LayerMask surfaceMask;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState =CursorLockMode.Locked;
        presentHealth=playerHealth;
        healthBar1.GivefullHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {   onSurface=Physics.CheckSphere(surfaceCheck.position,surfaceDistance,surfaceMask);
    if(onSurface && velocity.y<0){
        velocity.y =-2f;
    }


    velocity.y += gravity * Time.deltaTime;
    cC.Move(velocity*Time.deltaTime);


        playerMove();

    Jump();

    sprint();

    }




void playerMove()
    {   
        float Horizontal_axis =Input.GetAxisRaw("Horizontal");
        float vertical_axis =Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(Horizontal_axis,0f,vertical_axis).normalized;


        if (direction.magnitude>=0.1f){
            
            animator.SetBool("walk",true);
            animator.SetBool("running",false);
            animator.SetBool("idle",false);
            animator.SetTrigger("jump");
            animator.SetBool("aimWalk",false);
            animator.SetBool("idleAim",false);

            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+playerCamera.eulerAngles.y;
            float angle =Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turncalmVelocity,turncalmTime);
            transform.rotation=Quaternion.Euler(0f,angle,0f);
            Vector3 moveDirction =Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            cC.Move(moveDirction.normalized*playerSpeed*Time.deltaTime);
        }

        else
        {
            animator.SetBool("idle",true);
            animator.SetTrigger("jump");
            animator.SetBool("walk",false);
            animator.SetBool("running",false);
            animator.SetBool("aimWalk",false);
        }

    }







void Jump(){

    if (Input.GetButtonDown("Jump")&& onSurface)
    {   
         animator.SetBool("walk",false);
         animator.SetTrigger("jump");
          animator.SetBool("idle",false);

        velocity.y =Mathf.Sqrt(jumpRange * -2 * gravity);
    }
    else{
         animator.ResetTrigger("jump");
    }



}



void sprint()
    {   

        if(Input.GetButton("sprint") &&  Input.GetKey(KeyCode.W)  || Input.GetKey(KeyCode.UpArrow)  && onSurface)
        {
        
        float Horizontal_axis =Input.GetAxisRaw("Horizontal");
        float vertical_axis =Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(Horizontal_axis,0f,vertical_axis).normalized;


        if (direction.magnitude>=0.1f){
            
            
            animator.SetBool("idle",false);

            animator.SetBool("walk",false);
            animator.SetBool("running",true);
            animator.SetBool("idleAim",false);



            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg+playerCamera.eulerAngles.y;
            float angle =Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turncalmVelocity,turncalmTime);
            transform.rotation=Quaternion.Euler(0f,angle,0f);
            Vector3 moveDirction =Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            cC.Move(moveDirction.normalized*playersprint*Time.deltaTime);
            }


            else{

            animator.SetBool("idle",false);

            animator.SetBool("walk",false);

            }
        }
    }
int n=0;
    public void playerHitDamage(float takeDamage)
    {       n++;
            presentHealth-=takeDamage;
            healthBar1.SetHealth(presentHealth);
            if(n%5==0){
               audioSource.PlayOneShot(playerHurtSound);
            }

            if(presentHealth<=0){
                PlayerDie();
            }
    }


private void PlayerDie()
    {
            EndGameMenuUI.SetActive(true);
            Cursor.lockState=CursorLockMode.None;
            deathCam.SetActive(true);
            Object.Destroy(gameObject,1.0f);



    }







}








