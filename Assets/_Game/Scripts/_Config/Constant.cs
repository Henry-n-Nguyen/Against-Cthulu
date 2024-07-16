using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    // Input Action Constants
    public const string INPUT_ACTION_MOVING = "Move";
    public const string INPUT_ACTION_ATTACKING = "Attack";
    public const string INPUT_ACTION_JUMPING = "Jump";
    public const string INPUT_ACTION_HITTING = "Hit";

    // Trigger Anim Constants
    public const string TRIGGER_IDLE = "idle";
    public const string TRIGGER_WALK = "walk";
    public const string TRIGGER_RUN_START = "run(start)";
    public const string TRIGGER_RUN_END = "run(end)";

    public const string TRIGGER_JUMP_ATTACK = "jump(attack)";
    public const string TRIGGER_JUMP_START = "jump(start)";
    public const string TRIGGER_JUMP_END = "jump(end)";
    public const string TRIGGER_JUMP_LANDING = "jump(landing)";
    public const string TRIGGER_JUMP_RISE = "jump(rise)";
    public const string TRIGGER_JUMP_HIT = "jump(hit)";
    public const string TRIGGER_JUMP_HIT_GROUND = "jump(hitGround)";
    public const string TRIGGER_JUMP_UP_ATTACK = "jump(upAttack)";

    public const string TRIGGER_DEATH = "death";
    public const string TRIGGER_HIT = "hit";

    public const string TRIGGER_ATTACK_FIRST = "attack(first)";
    public const string TRIGGER_ATTACK_SECOND = "attack(second)";
    public const string TRIGGER_ATTACK_RECOVER = "attack(recover)";

    // Trigger Anim Constants 
    public const string TRIGGER_FLY = "fly";
    public const string TRIGGER_DESPAWN = "despawn";

    // Public Constants
    public const string TAG_PLAYER = "Player";

}
