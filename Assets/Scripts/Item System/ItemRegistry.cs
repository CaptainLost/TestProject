using System.Collections;
using System.Collections.Generic;

public class ItemRegistry : IEnumerable<Item>
{
    public ItemRegistry(ItemDatabase itemDatabase)
    {
        foreach (ItemModel itemModel in itemDatabase.Items)
        {
            Item item = new Item(itemModel);

            Items.Add(item);
        }
    }

    public List<Item> Items { get; private set; } = new List<Item>();

    public IEnumerator<Item> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
