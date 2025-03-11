using Zenject;

public class ItemSlotUI : BaseItemUI
{
    public IItemContainer SourceContainer { get; private set; }
    public ItemSlot CurrentItemSlot { get; private set; }

    public void DisplayItemSlot(IItemContainer sourceContainer, ItemSlot itemSlot)
    {
        SourceContainer = sourceContainer;
        CurrentItemSlot = itemSlot;

        SetImage(itemSlot.Item.Sprite);
    }

    public class Factory : PlaceholderFactory<ItemSlotUI>
    {

    }
}
