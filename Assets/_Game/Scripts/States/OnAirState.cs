using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {
        if (t.isGrounded)
        {
            Vector2 jumpVector = Vector2.up + Vector2.right * t.horizontal * 0.3f;
            t.rb.velocity = Vector2.zero;
            t.rb.AddForce(jumpVector * t.jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnExecute(AbstractCharacter t)
    {
        if (t.rb.velocity.y < -0.01f) Falling(t);
        else if (t.rb.velocity.y > 0.01f) Jumping(t);

        t.OnAir();
    }

    public void OnExit(AbstractCharacter t)
    {
        if (t.isJumping)
        {
            t.isJumping = false;
            t.ChangeAnim(Constant.ANIM_JUMP_LANDING);
        }
    }

    protected void Jumping(AbstractCharacter t)
    {
        t.ChangeAnim(Constant.ANIM_JUMP_START);
    }

    protected void Falling(AbstractCharacter t)
    {
        t.ChangeAnim(Constant.ANIM_JUMP_END);
    }
}
