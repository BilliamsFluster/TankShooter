using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredAI : AIController
{
    // Start is called before the first frame update
    public float fleeDistance;

    
   protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // based on attack sphere
        soundInHearingRange = Physics.CheckSphere(transform.position, hearingRange, whatIsSound); // based on hearing sphere

       
        playerInFleeRange = Physics.CheckSphere(transform.position, fleeRange, whatIsFLee); // based on flee sphere

        if (!InvestigatingSound)
        {
            if (!playerInAttackRange)
            {
                Patroling(); // Patroling state
                Passive(); // passive state
            }
        }
        if (soundInHearingRange) // investigating state
        {
            HeardSound();
            


        }
        if (InvestigatingSound) // investigating state
        {
            HeardSound();
        }




        if (playerInAttackRange) AttackPlayer(); // attacking state
        if (playerInFleeRange) FindFurthestWalkpoint();// flee from player
        //if (!playerInFleeRange) fleeing = false;
    }

    protected override void AttackPlayer()
    {
        
        if(!fleeing)
        {
            //enemy doesnt move
            agent.SetDestination(transform.position);
            InvestigatingSound = false;
            InvestigatingSound = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
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

            
            if (!alreadyAttacked)
            {
                if (gun != null)
                {
                    gun.ShootBullet();

                }

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks); // call the reset attack function after some specified time
            }
        }
       
    }


    void FindFurthestWalkpoint()
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
            fleeing = false;
        }
    }
   
    private void SearchWalkPoint()
    {
        //calculate random point in range

        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange,whatIsPlayer);

        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.tag == "Player")
            {
                float distance = Vector3.Distance(collider.gameObject.transform.position, transform.position);
                

               if((distance) < fleeRange)
                {
                    if(!fleeing)
                    {
                        
                       
                        fleeing = true;
                       

                        Vector3 fleeDirection =  collider.gameObject.transform.position - transform.position;
                        Debug.Log(collider.gameObject);
                        fleeDirection.Normalize();

                        transform.rotation = Quaternion.LookRotation(fleeDirection);
                       

                        walkPoint = new Vector3(transform.position.x - (fleeDirection.x * fleeDistance), transform.position.y, transform.position.z - (fleeDirection.z * fleeDistance) );
                        walkPointSet = true;
                    }
                    
                    
                }
                else
                {
                    fleeing = false;
                    AttackPlayer(); 
                }

               

            }
        }
 
    }


    



}
