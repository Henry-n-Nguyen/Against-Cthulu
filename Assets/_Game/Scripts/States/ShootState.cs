using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : IState<AbstractCharacter>
{
    float timer = 0f;

    public void OnEnter(AbstractCharacter t)
    {

    }
    public void OnExecute(AbstractCharacter t)
    {
        t.ActiveShoot();
    }

    public void OnExit(AbstractCharacter t)
    {

    }
}
