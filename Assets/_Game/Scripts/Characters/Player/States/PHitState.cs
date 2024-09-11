using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PHitState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {
        t.SetMove(Vector2.zero);
        t.ChangeState(AbstractCharacter.IDLE_STATE);
    }

    public void OnExecute(AbstractCharacter t)
    {

    }

    public void OnExit(AbstractCharacter t)
    {

    }

}
