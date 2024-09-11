using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDeadState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Death();
    }

    public void OnExit(AbstractCharacter t)
    {

    }
}
