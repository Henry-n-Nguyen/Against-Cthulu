using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class AttackState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Attack();
    }

    public void OnExit(AbstractCharacter t)
    {

    }

}
