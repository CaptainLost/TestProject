using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField]
    private EnemySettings m_enemySettings;

    public override void InstallBindings()
    {
        Container.Bind<EnemySettings>()
            .FromInstance(m_enemySettings)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<EnemySpawner>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<EnemyKilledGameFinish>()
            .AsSingle()
            .NonLazy();

        Container.BindFactory<EnemyActor, EnemyActor.Factory>()
            .FromComponentInNewPrefab(m_enemySettings.EnemyActorPrefab)
            .AsSingle();

        EnemySignalsInstaller.Install(Container);
    }
}
