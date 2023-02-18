using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TankController : Controller
{

    [Header("References")]
    private Transform orientation;
    private Transform player;
    private Transform playerObj;
    private Rigidbody rigidBody;
    private float rotationSpeed;

  

    // Start is called before the first frame update
    public override void Start() 
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
    public override void Update() 
    {
        
        base.Update();
       
    }

}
