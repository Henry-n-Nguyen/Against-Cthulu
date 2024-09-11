using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {

    }

    public void OnExecute(AbstractEnemy t)
    {
        t.PreAttack();
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
