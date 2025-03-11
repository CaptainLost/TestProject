using UnityEngine;
using Zenject;

public class EnemyActorDamagable : MonoBehaviour, IDamagable
{
    private IHealth m_health;
    private EnemyActorComponents m_actorComponents;

    private MaterialPropertyBlock m_propertyBlock;

    [Inject]
    private void Construct(IHealth health, EnemyActorComponents actorComponents)
    {
        m_health = health;
        m_actorComponents = actorComponents;
    }

    private void Awake()
    {
        m_propertyBlock = new MaterialPropertyBlock();
    }

    public void Damage(float damageAmount)
    {
        m_health.ReceiveDamage(damageAmount);
    }
}
