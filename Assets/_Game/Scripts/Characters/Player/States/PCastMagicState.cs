using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PCastMagicState : IState<AbstractCharacter>
{
    private float timer;

    public void OnEnter(AbstractCharacter t)
    {
        timer = 0f;

        if (t.IsJumping)
        {
            t.ChangeAnim(S_Constant.ANIM_JUMP_CAST_MAGIC);
            t.CastMagic();
            t.ChangeState(AbstractCharacter.ON_AIR_STATE);
        }
        else
        {
            t.SetMove(Vector2.zero);
            t.ChangeAnim(S_Constant.ANIM_CAST_MAGIC);
            t.CastMagic();
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }
    }

    public void OnExecute(AbstractCharacter t)
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            if (t.IsJumping) t.ChangeState(AbstractCharacter.ON_AIR_STATE);
            else t.ChangeState(AbstractCharacter.IDLE_STATE);
        }
    }

    public void OnExit(AbstractCharacter t)
    {

    }
}
