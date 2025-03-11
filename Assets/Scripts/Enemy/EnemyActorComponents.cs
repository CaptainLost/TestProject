using System;
using UnityEngine;

[Serializable]
public class EnemyActorComponents
{
    [field: SerializeField]
    public GameObject EnemyObject { get; private set; }
    [field: SerializeField]
    public MeshRenderer EnemyRenderer { get; private set; }
}
