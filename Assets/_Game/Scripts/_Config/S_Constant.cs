using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Constant
{
    // Input Action Constants
    public const string INPUT_ACTION_MOVING = "Move";
    public const string INPUT_ACTION_ATTACKING = "PreAttack";
    public const string INPUT_ACTION_JUMPING = "OnAir";
    public const string INPUT_ACTION_HITTING = "Hit";

    // Trigger Player's Anim Constants
    public const string ANIM_IDLE = "idle";
    public const string ANIM_WALK = "walk";

    public const string ANIM_RUN = "run";
    public const string ANIM_RUN_LOOP = "run(loop)";
    public const string ANIM_RUN_END = "run(end)";

    public const string ANIM_JUMP_START = "jump(start)";
    public const string ANIM_JUMP_END = "jump(end)";
    public const string ANIM_JUMP_LANDING = "jump(landing)";
    public const string ANIM_JUMP_RISE = "jump(rise)";
    public const string ANIM_JUMP_HIT = "jump(hit)";
    public const string ANIM_JUMP_HIT_GROUND = "jump(hitGround)";
    public const string ANIM_JUMP_UP_ATTACK = "jump(upAttack)";
    public const string ANIM_JUMP_SLIDE = "jump(slide)";

    public const string ANIM_DEATH = "death";
    public const string ANIM_HIT = "hit";

    public const string ANIM_ATTACK = "attack";
    public const string ANIM_ATTACK_FIRST = "attack(first)";
    public const string ANIM_ATTACK_SECOND = "attack(second)";
    public const string ANIM_ATTACK_RECOVER = "attack(recover)";

    public const string ANIM_CAST_MAGIC = "cast(magic)";
    public const string ANIM_JUMP_CAST_MAGIC = "jump_cast(magic)";

    public const string ANIM_SLIDE = "slide";

    // Trigger Others Anim Constants 
    public const string ANIM_TRIGGER = "trigger";
    public const string ANIM_DESPAWN = "despawn";

    // Public Constants
    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_GROUND = "Ground";
    public const string TAG_WALL = "Wall";

    // Public Layer
    public const string LAYER_GROUND = "Ground";
    public const string LAYER_WALL = "Wall,Ground";
}
