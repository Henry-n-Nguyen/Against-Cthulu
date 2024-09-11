using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EHitState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {
        t.SetMove(Vector2.zero);
        t.ChangeState(AbstractEnemy.E_IDLE_STATE);
    }

    public void OnExecute(AbstractEnemy t)
    {

    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
