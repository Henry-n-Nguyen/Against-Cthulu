using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PHitState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.ChangeAnimDirectly(S_Constant.ANIM_HIT);

        t.SetMove(Vector2.zero);
    }

    public void OnExecute(Player t)
    {

    }

    public void OnExit(Player t)
    {
        t.SetBool(CharacterState.Hit, false);
    }

}
