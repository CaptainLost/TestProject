using Zenject;

public class EnemySignalsInstaller : Installer<EnemySignalsInstaller>
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<EnemyKilledSignal>()
            .OptionalSubscriber();
    }
}
