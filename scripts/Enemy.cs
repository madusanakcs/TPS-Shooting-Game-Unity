using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
  [Header("Enemy Health and Damage")]
  private float enemyHealth =120f;
  private float presentHealth ;
  public float giveDamage=5f;
 

    [Header("Enemy Things")]
    public NavMeshAgent enemyAgent;
    public Transform LookPoint;
    public Camera ShootingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;



    [Header("Enemy Garding Var")]
    public GameObject[] walkPoints;
    int currentEnemyPosition = 0;
    public float enemySpeed = 1.5f;
    float walkingPoinRadius = 2;

    [Header("Sounds and UI")]
 
    public AudioClip shootingSound;

     public AudioSource audioSource;

    [Header("Enemy Shooting War")]
    public float timebtwShoot;
    bool previouslyShoot;


    [Header("Enemy Animation and Spark effect")]
      public Animator anime;
      public ParticleSystem muzzleSpark;


    [Header("Enemy mood / Situation")]
    public float visionRadius;
    public float ShootingRadius;
    public bool playerInVisionRadius;
    public bool playerInShootingRadius;





  private void Awake()
  {
    audioSource=GetComponent<AudioSource>();
    presentHealth=enemyHealth;
    playerBody = GameObject.Find("player2").transform;
    enemyAgent = GetComponent<NavMeshAgent>();
  }




private void Guard(){

  if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingPoinRadius)
  {
    currentEnemyPosition= Random.Range(0, walkPoints.Length);
    if(currentEnemyPosition >= walkPoints.Length)
    {
      currentEnemyPosition = 0;
    }
}
transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, enemySpeed * Time.deltaTime);

// change the direction of the enemy
transform.LookAt(walkPoints[currentEnemyPosition].transform.position);

}
  

private void ChasePlayer(){

  if(enemyAgent.SetDestination(playerBody.position))
  {
    // animations
    anime.SetBool("Walk",false);
     anime.SetBool("AimRun",true);
       anime.SetBool("Shoot",false);

       anime.SetBool("Die",false);
    //vision and shooting radius increase

    visionRadius = 30f;
    ShootingRadius = 15f;


  }

  else
  {
    anime.SetBool("Walk",false);
     anime.SetBool("AimRun",false);
       anime.SetBool("Shoot",false);
      anime.SetBool("AimDie",true);
       anime.SetBool("Die",true);
  }

}


private void ShootPlayer()
{
  enemyAgent.SetDestination(transform.position);
  transform.LookAt(LookPoint);

  if (!previouslyShoot)
  {

    muzzleSpark.Play();
    audioSource.PlayOneShot(shootingSound);

    RaycastHit hit;
    if (Physics.Raycast(ShootingRaycastArea.transform.position, ShootingRaycastArea.transform.forward, out hit, ShootingRadius))
    {

        Debug.Log("Shooting"+hit.transform.name);
        playerScript playerBody = hit .transform.GetComponent<playerScript>();

        if(playerBody!=null)
        {
          playerBody.playerHitDamage(giveDamage);
        }

          anime.SetBool("Walk",false);
     anime.SetBool("AimRun",false);
       anime.SetBool("Shoot",true);

       anime.SetBool("Die",false);
        
      
    }
    previouslyShoot=true;
    Invoke(nameof(ActiveShooting),timebtwShoot);
  }


}


private void ActiveShooting()
{
    previouslyShoot=false;  
}


public void enemyHitDamage(float takeDamage)
  {
    presentHealth-=takeDamage;

    if (presentHealth<=0){
      enemyDie();
          anime.SetBool("Walk",false);
     anime.SetBool("AimRun",false);
       anime.SetBool("Shoot",false);

       anime.SetBool("Die",true);
    }


  }


private void enemyDie()
{
  enemyAgent.SetDestination(transform.position);
  enemySpeed=0f;
  ShootingRadius=0f;
  visionRadius=0f;
  playerInShootingRadius=false;
  playerInVisionRadius=false;
  Object.Destroy(gameObject,5.0f);

}



  public void Update()
  {
    playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
    playerInShootingRadius = Physics.CheckSphere(transform.position, ShootingRadius, PlayerLayer);

    
  if (!playerInVisionRadius && !playerInShootingRadius) Guard();
  if (playerInVisionRadius && !playerInShootingRadius) ChasePlayer();
  if(playerInVisionRadius && playerInShootingRadius) ShootPlayer();
  
  }




}
