using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EIdleState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {
        t.SetBool(CharacterState.Run, false);
        t.SetBool(CharacterState.Attack, false);
        t.SetBool(CharacterState.Jump, false);
    }

    public void OnExecute(AbstractEnemy t)
    {
        t.Idle();
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
