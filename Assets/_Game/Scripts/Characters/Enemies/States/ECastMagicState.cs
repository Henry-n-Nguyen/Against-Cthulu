using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECastMagicState : IState<AbstractEnemy>
{
    public void OnEnter(AbstractEnemy t)
    {
        t.CastMagic();
    }

    public void OnExecute(AbstractEnemy t)
    {

    }

    public void OnExit(AbstractEnemy t)
    {

    }
}
