using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDeadState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {
        t.Death();
    }

    public void OnExecute(AbstractEnemy t)
    {
        
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
