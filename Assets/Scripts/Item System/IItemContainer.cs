using System;
using System.Collections.Generic;

public interface IItemContainer : IEnumerable<ItemSlot>
{
    event Action<ItemSlot> OnItemSlotAdded;
    event Action<ItemSlot> OnItemSlotModified;
    event Action<ItemSlot> OnItemSlotRemoved;

    bool CanAddItem(Item item, int amount);
    void AddItem(Item item, int amount);
    bool CanRemoveItem(Item item, int amount);
    void RemoveItem(Item item, int amount);

    bool TransferItem(IItemContainer targetContainer, Item item, int amount);
}
