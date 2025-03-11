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

        UpdateColorOnHit();
    }

    private void UpdateColorOnHit()
    {
        float healthPercent = Mathf.Clamp01(m_health.Health / m_health.MaxHealth);

        Color newColor = new Color(healthPercent * 255f, 0f, 0f);

        ChangePropertyColor(newColor);
    }

    private void ChangePropertyColor(Color color)
    {
        m_actorComponents.EnemyRenderer.GetPropertyBlock(m_propertyBlock);
        m_propertyBlock.SetColor("_Color", color);
        m_actorComponents.EnemyRenderer.SetPropertyBlock(m_propertyBlock);
    }
}
