using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ItemSlotTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ItemSlotUI m_itemSlotUI;

    private TooltipController m_tooltipController;
    private PlayerEquipment m_playerEquipment;

    private bool m_isOpen;

    [Inject]
    private void Construct(TooltipController tooltipController, PlayerEquipment playerEquipment)
    {
        m_tooltipController = tooltipController;
        m_playerEquipment = playerEquipment;
    }

    private void OnDisable()
    {
        if (m_isOpen)
            m_tooltipController.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemSlot itemSlot = m_itemSlotUI.CurrentItemSlot;

        if (itemSlot == null || !itemSlot.IsValid())
            return;

        ItemDescriptionBuilder descriptionBuilder = new ItemDescriptionBuilder(itemSlot.Item);

        descriptionBuilder
            .Category()
            .ComapredAttributesAll(m_playerEquipment);

        m_isOpen = true;
        m_tooltipController.Show(itemSlot.Item.Name, descriptionBuilder.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_isOpen = false;
        m_tooltipController.Hide();
    }
}
