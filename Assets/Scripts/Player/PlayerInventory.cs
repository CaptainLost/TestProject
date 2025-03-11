using System;
using Zenject;

public class PlayerInventory : IInitializable, IDisposable
{
    public ItemContainer ItemContainer { get; private set; } = new ItemContainer();

    private readonly ItemSystem m_itemSystem;
    private readonly SignalBus m_signalBus;

    public PlayerInventory(ItemSystem itemSystem, SignalBus signalBus)
    {
        m_itemSystem = itemSystem;
        m_signalBus = signalBus;
    }

    public void Initialize()
    {
        m_signalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    public void Dispose()
    {
        m_signalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }

    private void OnGameStarted(GameStartedSignal gameStartedSignal)
    {
        ItemContainer.Clear();

        foreach (Item item in m_itemSystem.ItemRegistry)
        {
            ItemContainer.AddItem(item, 1);
        }
    }
}