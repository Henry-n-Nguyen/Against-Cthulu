using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EIdleState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {

    }

    public void OnExecute(AbstractEnemy t)
    {
        t.Idle();
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
