using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
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
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
       
        agent = GetComponent<NavMeshAgent>();
        

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

        walkPoint = new Vector3(transform.position.x + randomRangeX, transform.position.y, transform.position.z + randomRangeZ);

        if(Physics.Raycast(walkPoint,-transform.up,2f,whatIsground))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        if(player != null)
        {
            agent.SetDestination(player.transform.position);
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
    void Start()
    {
        Invoke(nameof(UpdateComponents), 1f);
        
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
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange && !playerInSightRange) Patroling();
        if (!playerInAttackRange && playerInSightRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
