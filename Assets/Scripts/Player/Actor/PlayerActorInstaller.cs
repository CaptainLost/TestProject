using UnityEngine;
using Zenject;

public class PlayerActorInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerActorComponents m_playerComponents;

    public override void InstallBindings()
    {
        Container.Bind<PlayerActorComponents>()
            .FromInstance(m_playerComponents)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerActorMovement>()
            .AsSingle()
            .NonLazy();

        Container.Bind<PlayerActorCamera>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerActorInput>()
            .AsSingle()
            .NonLazy();

        Container.Bind<PlayerShooter>()
            .AsSingle()
            .NonLazy();
    }
}
