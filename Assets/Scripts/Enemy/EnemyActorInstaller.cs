using UnityEngine;
using Zenject;

public class EnemyActorInstaller : MonoInstaller
{
    [SerializeField]
    private EnemyActorComponents m_actorComponents;

    public override void InstallBindings()
    {
        Container.Bind<EnemyActorComponents>()
            .FromInstance(m_actorComponents)
            .AsSingle();

        Container.Bind<IHealth>()
            .To<EnemyActorHealth>()
            .AsSingle();

        Container.Bind<IDeathHandler>()
            .To<EnemyActorDeathHandler>()
            .AsSingle();
    }
}
