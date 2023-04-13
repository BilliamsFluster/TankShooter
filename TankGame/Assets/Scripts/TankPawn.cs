using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public KeyCode fireKey = KeyCode.Space;

    
    private Gun gun;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
        gun = gameObject.GetComponent<Gun>();
        GameManager.instance.TankPlayers.Add(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update(); 

        if (Input.GetKeyDown(fireKey)) // check if we are pressing fire key
        {
            gun.ShootBullet();
        }


    }

    
}
