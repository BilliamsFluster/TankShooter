using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : AIController
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
       
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // based on attack sphere
        soundInHearingRange = Physics.CheckSphere(transform.position, hearingRange, whatIsSound); // based on hearing sphere


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

        
        if (playerInAttackRange ) AttackPlayer(); // attacking state
    }
}
