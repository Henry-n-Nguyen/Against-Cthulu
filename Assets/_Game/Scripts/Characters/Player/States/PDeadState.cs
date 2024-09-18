using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDeadState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.Death();
    }

    public void OnExecute(Player t)
    {
        GatherDeathInput(t);
    }

    public void OnExit(Player t)
    {

    }

    private void GatherDeathInput(Player t)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            t.ChangeAnim(S_Constant.ANIM_IDLE);
            t.ChangeState(Player.IDLE_STATE);
        }
    }
}
