using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class HitState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Hit();
    }

    public void OnExit(AbstractCharacter t)
    {

    }

}
