using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class MoveState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {
        
    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Move();
    }

    public void OnExit(AbstractCharacter t)
    {

    }

}
