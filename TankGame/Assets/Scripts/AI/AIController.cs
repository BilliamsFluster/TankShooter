using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    public NavMeshAgent agent;

    [HideInInspector]
    public GameObject player;
    private GameObject GunObj;
    public Gun gun;
    public bool showDebugSpheres = false;
    public bool notifySpawnerToRespawn = false;
    Vector3 bulletSoundLocation = Vector3.zero;
    

    public LayerMask whatIsground, whatIsPlayer, whatIsSound, whatIsFLee;

    public Vector3 walkPoint;

    //Patroling 
    protected bool walkPointSet;
    public float walkPointRange;
    public List<GameObject> walkpoints;
    public float walkSpeed = 5f;
    public bool usePatrolPath;
    int walkpointIndex = 0;
    public bool canLoop = true;
    protected bool fleeing = false;
    
    

    //Attacking 
    public float timeBetweenAttacks;
    public float passiveAttackTime;
    public float aggressiveAttackTime;
    public float timeBeingAggressive;
    protected bool alreadyAttacked;

    //States
    public float sightRange, attackRange, hearingRange, fleeRange;
    public bool playerInSightRange, playerInAttackRange, soundInHearingRange, playerInFleeRange, InvestigatingSound, isAggressive = false;


    protected override void Awake()
    {
       
        agent = GetComponent<NavMeshAgent>();
        base.Awake();


    }
    void RandomPatrol()
    {
        if (!walkPointSet) SearchWalkPoint(); // find a walkpoint
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint); // set location to move towards to a random walkpoint

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint; // did we reach the walkpoint

        if (distanceToWalkPoint.magnitude <= 2f)
        {
            walkPointSet = false; // find another walkpoint
        }
    }
    void PathPatrol()
    {

        if(usePatrolPath)
        {
            Vector3 destination = walkpoints[walkpointIndex].transform.position; // get the current index of the walkpoint current position
            agent.SetDestination(destination); // set destination to current index

            float distance = Vector3.Distance(transform.position, destination); // did we reach the current index position 
            if (distance <= 1f) // if we did and we have a next valid index than increment the index
            {
                if (walkpointIndex < walkpoints.Count - 1)
                {
                    walkpointIndex++;

                }
                else
                {
                    if (canLoop) // if we reached the last index and can loop 
                    {
                        walkpointIndex = 0;
                    }
                }

            }
        }
        
    }
    protected void Patroling()
    {
        //options for patroling
        if (usePatrolPath)
        {
            PathPatrol(); // patrol on a path
        }
        else
        {
            RandomPatrol(); // use random points to patrol
            
        }
        

    }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomRangeZ = Random.Range(-walkPointRange, walkPointRange);
        float randomRangeX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomRangeX, transform.position.y, transform.position.z + randomRangeZ); // set walk point to the result of the random generated position

        if(Physics.Raycast(walkPoint,-transform.up,10f,whatIsground))
        {
           
            
        }
        walkPointSet = true;
          
    }

    protected void ChasePlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, hearingRange); // get all colliders within sphere
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {

                player = collider.gameObject; // cast the current hit collider game object to the bullet
                if (player != null)
                {
                    agent.SetDestination(player.transform.position); // set destination to player location
                }
                break;
            }
        }
       
        
        
    }

    protected virtual void AttackPlayer()
    {
        
        //enemy doesnt move
        agent.SetDestination(transform.position);
        InvestigatingSound = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange); // get all colliders within sphere
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {

                GameObject player = collider.gameObject; // cast the current hit collider game object to the bullet
                if (player != null)
                {
                    transform.LookAt(player.transform.position);

                }
                break;
            }
        }
       
        if(!alreadyAttacked)
        {
           if(gun != null)
            {
                gun.ShootBullet();
               
            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // call the reset attack function after some specified time
        }
    }
    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }





    // Start is called before the first frame update
    protected override void Start()
    {
        Invoke(nameof(UpdateComponents), 1f);
        
        
        
        passiveAttackTime = 1f;
        aggressiveAttackTime = 0.5f;
        timeBeingAggressive = 6f;

        timeBetweenAttacks = passiveAttackTime; // normal attack time
        InvestigatingSound = false; // not investigating sound




    }
    void UpdateComponents()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.enemies != null)
            {
                GameManager.instance.enemies.Add(this); // add ai controller to list
            }
           // player = GameManager.instance.newPawnObj;
            gun = GetComponent<Gun>();

        }
    }

    public void Aggressive()
    {

       timeBetweenAttacks = aggressiveAttackTime; // shorten attack time
       Invoke(nameof(Passive), timeBeingAggressive); // aggression cooldown
        
       
    }
    public void Passive()
    {
        timeBetweenAttacks = passiveAttackTime;
        
    }
    protected void HeardSound()
    {
        if (!playerInAttackRange && !playerInSightRange)
        {
            
            InvestigatingSound = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, hearingRange); // get all colliders within sphere
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag == "Bullet")
                {
                    
                    Bullet bulletObj = collider.gameObject.GetComponent<Bullet>(); // cast the current hit collider game object to the bullet
                    if (bulletObj !=null)
                    {
                        bulletSoundLocation = bulletObj.startLocation; // get the bullet location
                        agent.SetDestination(bulletSoundLocation); // set the destination to that bullet location
                        
                    }
                    break;
                }
            }
            var DistanceFromSound = Vector3.Distance(transform.position, bulletSoundLocation);

            if (DistanceFromSound <= 1) // did we finish traveling to the bullet heard location
            {
                InvestigatingSound = false; // if we did than we dont need to investigate
            }
        }
        else
        {
            InvestigatingSound = false;
        }
       
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.enemies != null)
            {
                GameManager.instance.enemies.Remove(this); // Remove this ai controller from the list
            }
        }
    }


    // Update is called once per frame
    protected override void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // based on sight sphere
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // based on attack sphere
        soundInHearingRange = Physics.CheckSphere(transform.position, hearingRange, whatIsSound); // based on hearing sphere

        
        if(!InvestigatingSound)
        {
            if (!playerInAttackRange && !playerInSightRange)
            {
                Patroling(); // Patroling state
                Passive(); // passive state
            }
        }
        if(soundInHearingRange) // investigating state
        {
            HeardSound();
            
        }
        if(InvestigatingSound) // investigating state
        {
            HeardSound();
        }

        if (!playerInAttackRange && playerInSightRange) ChasePlayer(); // Chasing state
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); // attacking state
        
        

    }
    // debug function
    private void OnDrawGizmos()
    {
        if(showDebugSpheres)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hearingRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
        
       
    }
    
}
