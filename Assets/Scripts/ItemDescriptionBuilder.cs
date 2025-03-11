using System.Text;
using UnityEngine;

public class ItemDescriptionBuilder
{
    private readonly StringBuilder m_stringBuilder = new StringBuilder();

    private readonly Item m_describedItem;

    public ItemDescriptionBuilder(Item item)
    {
        m_describedItem = item;
    }

    public override string ToString()
    {
        return m_stringBuilder.ToString();
    }

    public ItemDescriptionBuilder Category()
    {
        m_stringBuilder.AppendLine(m_describedItem.CategoryName);

        return this;
    }

    public ItemDescriptionBuilder ComapredAttributesAll(PlayerEquipment playerEquipment)
    {
        ItemSlot equipmentSlot = playerEquipment?.GetItemSlotFromCategory(m_describedItem.Category);
        Item equippedItem = (equipmentSlot != null && equipmentSlot.IsValid() && equipmentSlot.Item != m_describedItem)
            ? equipmentSlot.Item : null;

        ComparedAttribute("Move speed", m_describedItem.MoveSpeedModifier, equippedItem?.MoveSpeedModifier, true);
        ComparedAttribute("Damage", m_describedItem.AdditionalDamage, equippedItem?.AdditionalDamage, false);

        return this;
    }

    public ItemDescriptionBuilder ComparedAttribute(string attributeName, float itemValue, float? equippedValue, bool isPercentage)
    {
        m_stringBuilder.Append(attributeName);
        m_stringBuilder.Append(": ");

        string valueFormat = isPercentage ? "{0:F2}%" : "{0:F2}";

        if (equippedValue.HasValue)
        {
            if (itemValue != equippedValue.Value)
            {
                Color valueColor = itemValue > equippedValue.Value ? Color.green : Color.red;

                AppendColorized(string.Format(valueFormat, itemValue), valueColor);
                AppendColorized(" -> ", valueColor);
                AppendColorized(string.Format(valueFormat, equippedValue.Value), valueColor);
            }
            else
            {
                m_stringBuilder.Append(string.Format(valueFormat, itemValue));
                m_stringBuilder.Append(" == ");
                m_stringBuilder.Append(string.Format(valueFormat, equippedValue.Value));
            }
        }
        else
        {
            m_stringBuilder.Append(string.Format(valueFormat, itemValue));
        }

        m_stringBuilder.AppendLine();

        return this;
    }

    public ItemDescriptionBuilder Attributes()
    {
        m_stringBuilder.Append("Move speed: ");
        AppendColorized(m_describedItem.MoveSpeedModifier.ToString("F2"), Color.red);
        m_stringBuilder.AppendLine("%");

        return this;
    }

    public ItemDescriptionBuilder AppendColorized<T>(T appendObject, Color color)
    {
        m_stringBuilder.Append($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>");
        m_stringBuilder.Append(appendObject);
        m_stringBuilder.Append("</color>");

        return this;
    }
}
