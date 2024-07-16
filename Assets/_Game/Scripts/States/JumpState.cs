using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Jump();
    }

    public void OnExit(AbstractCharacter t)
    {

    }
}
