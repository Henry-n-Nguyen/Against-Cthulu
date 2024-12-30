using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {
        t.PreAttack();
    }

    public void OnExecute(AbstractEnemy t)
    {
        
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
