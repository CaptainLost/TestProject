using System;
using Zenject;

public class EnemyKilledGameFinish : IInitializable, IDisposable
{
    private readonly SignalBus m_signalBus;
    private readonly EnemySettings m_enemySettings;
    private readonly GameManager m_gameManager;

    private int m_aliveEnemies;

    public EnemyKilledGameFinish(SignalBus signalBus, EnemySettings enemySettings, GameManager gameManager)
    {
        m_signalBus = signalBus;
        m_enemySettings = enemySettings;
        m_gameManager = gameManager;
    }

    public void Initialize()
    {
        m_signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        m_signalBus.Subscribe<EnemyKilledSignal>(OnEnemyKilled);
    }

    public void Dispose()
    {
        m_signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        m_signalBus.Unsubscribe<EnemyKilledSignal>(OnEnemyKilled);
    }

    private void OnGameStarted(GameStartedSignal gameStartedSignal)
    {
        m_aliveEnemies = m_enemySettings.EnemiesToSpawn;
    }

    private void OnEnemyKilled(EnemyKilledSignal enemyKilledSignal)
    {
        m_aliveEnemies--;

        if (m_aliveEnemies <= 0)
        {
            m_gameManager.FinishGame();
        }
    }
}
