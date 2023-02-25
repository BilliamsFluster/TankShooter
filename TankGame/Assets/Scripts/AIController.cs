using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    public NavMeshAgent agent;

    public GameObject player;
    private GameObject GunObj;
    public Gun gun;

    public LayerMask whatIsground, whatIsPlayer;

    public Vector3 walkPoint;

    //Patroling 
    bool walkPointSet;
    public float walkPointRange;

    //Attacking 
    private float timeBetweenAttacks;
    public float passiveAttackTime;
    public float aggressiveAttackTime;
    public float timeBeingAggressive;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, isAggressive = false;

    protected override void Awake()
    {
       
        agent = GetComponent<NavMeshAgent>();
        base.Awake();
        

    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude <= 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomRangeZ = Random.Range(-walkPointRange, walkPointRange);
        float randomRangeX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomRangeX, transform.position.y, transform.position.z + randomRangeZ); // set walk point to the result of the random generated position

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsground))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        if(player != null)
        {
            agent.SetDestination(player.transform.position); // set destination to player location
        }
        
        
    }

    private void AttackPlayer()
    {
        //enemy doesnt move
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform.position);
        if(!alreadyAttacked)
        {
           if(gun != null)
            {
                gun.ShootBullet();
                GameManager.instance.PlayTankShotSound(); // play a shot sound
            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // call the reset attack function after some specified time
        }
    }
    private void ResetAttack()
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

        timeBetweenAttacks = passiveAttackTime;
        


    }
    void UpdateComponents()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.enemies != null)
            {
                GameManager.instance.enemies.Add(this); // add ai controller to list
            }
            player = GameManager.instance.newPawnObj;
            GunObj = GameObject.Find("TankAI");
            gun = GunObj.GetComponent<Gun>();

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

        if (!playerInAttackRange && !playerInSightRange)
        {
            Patroling(); // Patroling state
            Passive(); // passive state
        }
        if (!playerInAttackRange && playerInSightRange) ChasePlayer(); // Chasing state
        if (playerInAttackRange && playerInSightRange) AttackPlayer(); // attacking state
        
        

    }
    // debug function
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
