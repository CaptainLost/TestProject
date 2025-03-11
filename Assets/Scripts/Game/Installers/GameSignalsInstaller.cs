using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GamePrepareStartedSignal>()
            .OptionalSubscriber();

        Container.DeclareSignal<GamePrepareFailedSignal>()
            .OptionalSubscriber();

        Container.DeclareSignal<GameStartedSignal>()
            .OptionalSubscriber();

        Container.DeclareSignal<GameFinishedSignal>()
            .OptionalSubscriber();

        Container.DeclareSignal<GameLockCursor>()
            .OptionalSubscriber();

        Container.DeclareSignal<GameUnlockCursor>()
            .OptionalSubscriber();
    }
}
