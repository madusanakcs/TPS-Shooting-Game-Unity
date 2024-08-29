using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera camera;
    public float giveDamageOf =10f;
    public float shootingRange =100f;
    public float fireCharge = 15f;
    public Animator animator;
    public playerScript player;

   [Header("Rifle Animation and Shooting")]
   private float nextTimeToShoot = 0f;
   private int maxAmmo = 20;
   private int mag=15;
   private int presentAmmo;
   public float reloadTime = 1.3f;
   private bool setReloading = false;

    [Header("Rifle Effect")]
    public ParticleSystem muzzleSpark;
    public GameObject impactEffect;
    public GameObject goreEffect;
     public GameObject droneEffect;




     [Header("Sounds and UI ")]

        [SerializeField] private GameObject AmmoOutUI;
         [SerializeField] private  int timeToShowUI=1;

     
    
     public AudioClip reloadingSound;
    public AudioClip shootingSound;

     public AudioSource audioSource;


    private void Awake() {
        presentAmmo = maxAmmo;
    }
    void Update()
    {   
        if(setReloading)
        return;

        if(presentAmmo<=0)
        {
            StartCoroutine(Reloading());
            return;
        }

        if(Input.GetButton("Fire1") && Time.time >=nextTimeToShoot)
        {   
            animator.SetBool("fire" , true);
            animator.SetBool("idle" , false);

            nextTimeToShoot = Time.time + 1f/fireCharge;
            Shoot();
        }


        else if(Input.GetButton("Fire1") &&  Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
             animator.SetBool("idle" , false);
             animator.SetBool("idleAim" , true);
             animator.SetBool("fireWalk" , true);
             animator.SetBool("walk" , true);
                        animator.SetBool("reloading" , false);
          
        }

        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("idle" , false);
             animator.SetBool("idleAim" , true);
             animator.SetBool("fireWalk" , true);
             animator.SetBool("walk" , false);
                        animator.SetBool("reloading" , false);
          

        }



        else
        {
            animator.SetBool("fire" , false);
            animator.SetBool("idle" , true);
            animator.SetBool("reloading" , false);
            animator.SetBool("fireWalk" , false);
        }

    }

    void Shoot()
    {   
        //check for mag
        if(mag==0)
        {
            // show ammo out of text
            StartCoroutine(ShowAmmoOut());
            return;
        }

        presentAmmo--;

        if(presentAmmo==0)
        {
            mag--;

        }

        // updating UI
        AmmoCount.occurence.UpdateAmmoText(presentAmmo);
        AmmoCount.occurence.MagText(mag);

        muzzleSpark.Play();
         audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;
        if(Physics.Raycast(camera.transform.position , camera.transform.forward , out hitInfo , shootingRange))
        {

            Debug.Log(hitInfo.transform.name);
            
            Objects objects = hitInfo.transform.GetComponent<Objects>();
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Drone drone = hitInfo.transform.GetComponent<Drone>();



            if (objects != null){

                objects.objectHitDamage(giveDamageOf);
                 GameObject impactGO = Instantiate(impactEffect , hitInfo.point , Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO , 2f);
   
            }

            else if(enemy!=null)
            {

                enemy.enemyHitDamage(giveDamageOf);
                GameObject impactGO = Instantiate(goreEffect , hitInfo.point , Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO , 2f);

            }


            else if (drone!=null)
            {
                
                drone.DroneHitDamage(giveDamageOf);
                GameObject impactGO = Instantiate(droneEffect , hitInfo.point , Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO , 2f);

            }
            
        }

    }

    IEnumerator Reloading()
    {
        player.playerSpeed = 0f;
        player.playersprint = 0f;
        setReloading = true;
        Debug.Log("Reloading");
        animator.SetBool("reloading" , true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadTime );
        animator.SetBool("reloading" , false);
        presentAmmo = maxAmmo;
        player.playerSpeed=1.9f;
        player.playersprint=3f;
        setReloading = false;
    }
    IEnumerator ShowAmmoOut()
    {
        AmmoOutUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        AmmoOutUI.SetActive(false);
    }
}
