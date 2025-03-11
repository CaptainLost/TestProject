using Zenject;

public class PlayerSignalsInstaller : Installer<PlayerSignalsInstaller>
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<PlayerStatisticsCalculatedSignal>()
            .OptionalSubscriber();
    }
}
