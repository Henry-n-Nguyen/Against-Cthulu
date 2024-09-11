using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDeadState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {

    }

    public void OnExecute(AbstractEnemy t)
    {
        t.Death();
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
