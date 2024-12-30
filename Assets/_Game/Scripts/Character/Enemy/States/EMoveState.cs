using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMoveState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {

    }

    public void OnExecute(AbstractEnemy t)
    {
        t.Move();
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
