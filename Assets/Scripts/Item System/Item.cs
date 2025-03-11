using System;
using UnityEngine;

public class Item
{
    public string Name => m_itemModel.Name;
    public string CategoryName => m_itemModel.Category;
    public float MoveSpeedModifier => m_itemModel.MovementSpeed;
    public float AdditionalDamage => m_itemModel.Damage;
    public ItemCategory Category { get; private set; }
    public Sprite Sprite { get; private set; }

    private readonly ItemModel m_itemModel;

    public Item(ItemModel itemModel)
    {
        m_itemModel = itemModel;

        LoadSprite();
        LoadCategory();
    }

    private void LoadSprite()
    {
        string resourcePath = $"Items/{CategoryName}/{Name}";

        Sprite = Resources.Load<Sprite>(resourcePath);

        if (Sprite == null)
        {
            Debug.LogWarning($"Failed to load sprite for item {Name}");
        }
    }

    private void LoadCategory()
    {
        if (!Enum.TryParse(CategoryName, out ItemCategory category))
        {
            Debug.LogWarning($"Failed to parse category for item {Name}");

            return;
        }

        Category = category;
    }
}