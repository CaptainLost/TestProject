using System;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer : IItemContainer
{
    public event Action<ItemSlot> OnItemSlotAdded;
    public event Action<ItemSlot> OnItemSlotModified;
    public event Action<ItemSlot> OnItemSlotRemoved;

    private List<ItemSlot> m_itemSlots = new List<ItemSlot>();

    public IEnumerator<ItemSlot> GetEnumerator()
    {
        return m_itemSlots.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool CanAddItem(Item item, int amount)
    {
        return ItemSlot.IsValid(item, amount);
    }

    public void AddItem(Item item, int amount)
    {
        if (!CanAddItem(item, amount))
            return;

        ItemSlot itemSlot = m_itemSlots.Find(slot => slot.Item.Equals(item));

        if (itemSlot != null)
        {
            itemSlot.SetAmount(itemSlot.Amount + amount);
            OnItemSlotModified?.Invoke(itemSlot);
        }
        else
        {
            itemSlot = new ItemSlot(item, amount);

            m_itemSlots.Add(itemSlot);
            OnItemSlotAdded?.Invoke(itemSlot);
        }
    }

    public bool CanRemoveItem(Item item, int amount)
    {
        if (!ItemSlot.IsValid(item, amount))
            return false;

        return m_itemSlots.Exists(slot => slot.Item.Equals(item) && slot.Amount >= amount);
    }

    public void RemoveItem(Item item, int amount)
    {
        if (!CanRemoveItem(item, amount))
            return;

        // We assume that the object always exists
        ItemSlot itemSlot = m_itemSlots.Find(slot => slot.Item.Equals(item));

        itemSlot.SetAmount(itemSlot.Amount - amount);

        if (itemSlot.Amount <= 0)
        {
            m_itemSlots.Remove(itemSlot);
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

    public void Clear()
    {
        foreach (ItemSlot itemSlot in m_itemSlots)
        {
            OnItemSlotRemoved?.Invoke(itemSlot);
        }

        m_itemSlots.Clear();
    }
}
