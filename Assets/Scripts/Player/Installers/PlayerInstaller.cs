using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerSettingsSO m_playerSettings;

    public override void InstallBindings()
    {
        Container.Bind<IPlayer>()
            .To<PlayerFacade>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerInventory>()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerEquipment>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerStatistics>()
            .AsSingle()
            .NonLazy();

        Container.Bind<PlayerSettingsSO>()
            .FromInstance(m_playerSettings)
            .AsSingle();

        PlayerSignalsInstaller.Install(Container);
    }
}