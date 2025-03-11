using UnityEngine;
using Zenject;

public class EnemyActorDeathHandler : IDeathHandler
{
    private readonly EnemyActorComponents m_actorComponents;
    private readonly SignalBus m_signalBus;

    public EnemyActorDeathHandler(EnemyActorComponents actorComponents, SignalBus signalBus)
    {
        m_actorComponents = actorComponents;
        m_signalBus = signalBus;
    }

    public void Die()
    {
        GameObject.Destroy(m_actorComponents.EnemyObject);

        m_signalBus.Fire(new EnemyKilledSignal());
    }
}
