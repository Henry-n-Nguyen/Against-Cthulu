using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class MoveState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Move();
    }

    public void OnExit(AbstractCharacter t)
    {
        if (t.isRunning)
        {
            t.isRunning = false;

            if (!t.isJumping)
            {
                if (Mathf.Abs(t.rb.velocity.x) > t.walkSpeed - 0.1f)
                {
                    t.ChangeAnim(Constant.ANIM_RUN_END);
                }
                else
                {
                    t.ChangeAnim(Constant.ANIM_IDLE);
                }

                t.rb.velocity = Vector2.zero;
            } 
        }
    }

}
