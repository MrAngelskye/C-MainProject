using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff
{
    public float Duration { get; set; }
    public float MoveSpeedModifier { get; set; }
    public float AttackSpeedModifier { get; set; }

    public BuffDebuff(float duration, float moveSpeedModifier, float attackSpeedModifier)
    {
        Duration = duration;
        MoveSpeedModifier = moveSpeedModifier;
        AttackSpeedModifier = attackSpeedModifier;
    }
}

