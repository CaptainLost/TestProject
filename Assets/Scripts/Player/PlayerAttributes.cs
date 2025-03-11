using System;
using UnityEngine;

[Serializable]
public struct PlayerAttributes
{
    [Header("Move speed")]
    public float BaseMoveSpeed;
    public float MoveSpeedModifier;
    public float MoveSpeed;

    [Header("Damage")]
    public float BaseDamage;
    public float AdditionalDamage;
    public float Damage;
}
