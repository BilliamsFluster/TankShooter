using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TankController : Controller
{
    

    // Start is called before the first frame update
    protected override void Start() 
    {
        
        Cursor.lockState = CursorLockMode.Locked; // hide the cursor
        Cursor.visible = false;

        if(GameManager.instance != null)
        {
            if(GameManager.instance.players != null)
            {
                GameManager.instance.players.Add(this); // add tank controller to list
            }
        }

        base.Start();
    }



    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Remove(this); // Remove this tank controller from the list
            }
        }
    }

    // Update is called once per frame
    protected override void Update() 
    {
        
        base.Update();
       
    }
    protected override void Awake()
    {
        base.Awake();
    }

 
}
