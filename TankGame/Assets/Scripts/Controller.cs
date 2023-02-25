using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    [HideInInspector]
    public Pawn pawn;
    //public AudioClip impactSound;
    //public AudioClip shotSound;
    //public static AudioSource impactSource;
    //public static AudioSource shotSource;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        

    }
    protected virtual void Awake()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    
   
}
