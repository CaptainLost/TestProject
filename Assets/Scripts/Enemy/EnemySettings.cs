using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySettings
{
    [field: SerializeField]
    public EnemyActor EnemyActorPrefab { get; private set; }
    [field: SerializeField]
    public List<Transform> SpawnPositions { get; private set; }
    [field: SerializeField]
    public int EnemiesToSpawn { get; private set; }
    [field: SerializeField]
    public float MinHealth { get; private set; }
    [field: SerializeField]
    public float MaxHealth { get; private set; }
}
