using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<ItemSystem>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameCursorController>()
            .AsSingle()
            .NonLazy();

        GameSignalsInstaller.Install(Container);
    }
}