using UnityEngine;

public class PlayerShooter
{
    private readonly PlayerActorComponents m_actorComponents;
    private readonly PlayerStatistics m_playerStatistics;

    public PlayerShooter(PlayerActorComponents actorComponents, PlayerStatistics playerStatistics)
    {
        m_actorComponents = actorComponents;
        m_playerStatistics = playerStatistics;
    }

    public void Fire()
    {
        Vector3 fireOrigin = m_actorComponents.CharacterCamera.transform.position;
        Vector3 fireDirection = m_actorComponents.CharacterCamera.transform.forward;

        if (!Physics.Raycast(fireOrigin, fireDirection, out RaycastHit hitInfo))
            return;

        if (!hitInfo.collider.TryGetComponent(out IDamagable damagable))
            return;

        damagable.Damage(CalculateDamage());
    }

    private float CalculateDamage()
    {
        return m_playerStatistics.Attributes.Damage;
    }
}
