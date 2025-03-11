using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class PlayerEquipment : IItemContainer, IInitializable, IDisposable
{
    public event Action<ItemSlot> OnItemSlotAdded;
    public event Action<ItemSlot> OnItemSlotModified;
    public event Action<ItemSlot> OnItemSlotRemoved;

    private SignalBus m_signalBus;

    private Dictionary<ItemCategory, ItemSlot> m_equippedItems = new Dictionary<ItemCategory, ItemSlot>();

    public PlayerEquipment(SignalBus signalBus)
    {
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

    public IEnumerator<ItemSlot> GetEnumerator()
    {
        return m_equippedItems.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool CanAddItem(Item item, int amount)
    {
        if (!ItemSlot.IsValid(item, amount))
            return false;

        if (!m_equippedItems.ContainsKey(item.Category))
            return true;

        ItemSlot itemSlot = m_equippedItems[item.Category];

        if (itemSlot.IsValid() && itemSlot.Item != item)
            return false;

        // For the time being, we are not considering a maximum stack
        bool canFitItem = itemSlot.Amount + amount <= 1;

        return canFitItem;
    }

    public void AddItem(Item item, int amount)
    {
        if (!CanAddItem(item, amount))
            return;

        if (m_equippedItems.TryGetValue(item.Category, out ItemSlot itemSlot))
        {
            itemSlot.SetItem(item);
            itemSlot.SetAmount(itemSlot.Amount + amount);

            OnItemSlotModified?.Invoke(itemSlot);
        }
        else
        {
            itemSlot = new ItemSlot(item, amount);
            m_equippedItems.Add(item.Category, itemSlot);

            OnItemSlotAdded?.Invoke(itemSlot);
        }
    }

    public bool CanRemoveItem(Item item, int amount)
    {
        if (!ItemSlot.IsValid(item, amount))
            return false;

        return m_equippedItems.TryGetValue(item.Category, out ItemSlot itemSlot)
           && itemSlot.Item.Equals(item)
           && itemSlot.Amount >= amount;
    }

    public void RemoveItem(Item item, int amount)
    {
        if (!CanRemoveItem(item, amount))
            return;

        ItemSlot itemSlot = m_equippedItems[item.Category];

        itemSlot.SetAmount(itemSlot.Amount - amount);

        if (itemSlot.Amount <= 0)
        {
            m_equippedItems.Remove(item.Category);
            OnItemSlotRemoved?.Invoke(itemSlot);
        }
        else
        {
            OnItemSlotModified?.Invoke(itemSlot);
        }
    }

    public bool TransferItem(IItemContainer targetContainer, Item item, int amount)
    {
        if (!CanRemoveItem(item, amount) || !targetContainer.CanAddItem(item, amount))
            return false;

        RemoveItem(item, amount);
        targetContainer.AddItem(item, amount);

        return true;
    }

    public ItemSlot GetItemSlotFromCategory(ItemCategory itemCategory)
    {
        if (!m_equippedItems.TryGetValue(itemCategory, out ItemSlot itemSlot))
            return null;

        return itemSlot;
    }

    // For the time being, we are not considering a maximum stack
    public void EquipItem(IItemContainer sourceContainer, ItemSlot itemSlot)
    {
        if (!sourceContainer.CanRemoveItem(itemSlot.Item, 1))
            return;

        if (!CanAddItem(itemSlot.Item, 1))
        {
            // Replace Items


            return;
        }

        AddItem(itemSlot.Item, 1);
        sourceContainer.RemoveItem(itemSlot.Item, 1);
    }

    private void OnGameStarted(GameStartedSignal gameStartedSignal)
    {
        List<ItemSlot> itemsToRemove = new List<ItemSlot>();
        itemsToRemove.AddRange(m_equippedItems.Values);

        foreach (ItemSlot itemSlot in itemsToRemove)
        {
            RemoveItem(itemSlot.Item, itemSlot.Amount);
        }
    }
}
