public class ItemSlot
{
    public ItemSlot()
    {

    }

    public ItemSlot(Item item, int amount)
    {
        Item = item;
        Amount = amount;
    }

    public Item Item { get; private set; }
    public int Amount { get; private set; }

    public static bool IsValid(Item item, int amount)
    {
        return item != null && amount > 0;
    }

    public bool IsValid()
    {
        return IsValid(Item, Amount);
    }

    public void SetItem(Item item)
    {
        Item = item;
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    public void Swap(ItemSlot otherSlot)
    {
        Item otherItem = Item;
        int otherAmount = otherSlot.Amount;

        otherSlot.Item = Item;
        otherSlot.Amount = Amount;

        Item = otherItem;
        Amount = otherAmount;
    }
}
