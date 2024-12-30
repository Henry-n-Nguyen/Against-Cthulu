using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EExhaustedState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {

    }

    public void OnExecute(AbstractEnemy t)
    {
        t.Exhaust();
    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
