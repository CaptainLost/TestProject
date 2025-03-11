using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : IInitializable, IDisposable
{
    private readonly EnemyActor.Factory m_enemyFactory;
    private readonly SignalBus m_signalBus;
    private readonly EnemySettings m_enemySettings;

    public EnemySpawner(EnemyActor.Factory enemyFactory, SignalBus signalBus, EnemySettings enemySettings)
    {
        m_enemyFactory = enemyFactory;
        m_signalBus = signalBus;
        m_enemySettings = enemySettings;
    }

    public void Initialize()
    {
        m_signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    public void Dispose()
    {
        m_signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }

    public void SpawnEnemies()
    {
        if (m_enemySettings.SpawnPositions.Count < m_enemySettings.EnemiesToSpawn)
        {
            Debug.LogWarning("Failed to spawn enemies, not enought spawn points");

            return;
        }

        List<Transform> spawnPoints = GetSpawnPoints();

        foreach (Transform spawnPoint in spawnPoints)
        {
            EnemyActor enemyActor = m_enemyFactory.Create();
            enemyActor.transform.position = spawnPoint.position;
        }
    }

    private void OnGameStarted(GameStartedSignal gameStartedSignal)
    {
        SpawnEnemies();
    }

    private List<Transform> GetSpawnPoints()
    {
        List<Transform> spawnPositionPool = new List<Transform>(m_enemySettings.SpawnPositions);
        List<Transform> selectedPositions = new List<Transform>();

        for (int i = 0; i < m_enemySettings.EnemiesToSpawn; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, spawnPositionPool.Count);

            Transform selectedTransform = spawnPositionPool[randomIndex];

            selectedPositions.Add(selectedTransform);
            spawnPositionPool.RemoveAt(randomIndex);
        }

        return selectedPositions;
    }
}
