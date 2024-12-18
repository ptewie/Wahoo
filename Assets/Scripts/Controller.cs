using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    
    protected virtual void Start()
    {
        if (pawn != null) 
        {
            PossessPawn(pawn);
        }
    }
    
    protected virtual void Update()
    {
        MakeDecisions();
    }
    
    protected abstract void MakeDecisions();
    
    public virtual void PossessPawn(Pawn pawnToPossess)
    {
        pawn = pawnToPossess;
        pawn.controller = this;
        
        pawn.gameObject.layer = this.gameObject.layer;  
    }
    
    public virtual void UnpossessPawn()
    {
        pawn.controller = null;
        pawn = null;
    }
}