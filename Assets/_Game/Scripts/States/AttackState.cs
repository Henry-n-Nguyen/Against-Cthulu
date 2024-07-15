using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<AbstractCharacter>
{
    float timer = 0f;

    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.ActiveAttack();
    }

    public void OnExit(AbstractCharacter t)
    {

    }
}
