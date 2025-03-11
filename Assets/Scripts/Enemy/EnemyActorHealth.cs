public class EnemyActorHealth : IHealth
{
    public float MaxHealth { get; private set; }
    public float Health { get; private set; }

    private readonly EnemySettings m_enemySettings;
    private readonly IDeathHandler m_deathHandler;

    public EnemyActorHealth(EnemySettings enemySettings, IDeathHandler deathHandler)
    {
        m_enemySettings = enemySettings;
        m_deathHandler = deathHandler;

        SetInitialHealth();
    }

    public void ReceiveDamage(float damageValue)
    {
        Health -= damageValue;

        if (Health <= 0f)
        {
            m_deathHandler.Die();
        }
    }

    private void SetInitialHealth()
    {
        MaxHealth = UnityEngine.Random.Range(m_enemySettings.MinHealth, m_enemySettings.MaxHealth);
        Health = MaxHealth;
    }
}
