using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class EquipmentDropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private PlayerEquipmentSlotUI m_equipmentSlotUI;

    private DraggedItemUI m_dragSlotUI;
    private PlayerEquipment m_playerEquipment;

    [Inject]
    private void Construct(DraggedItemUI dragSlotUI, PlayerEquipment playerEquipment)
    {
        m_dragSlotUI = dragSlotUI;
        m_playerEquipment = playerEquipment;
    }

    public void OnDrop(PointerEventData eventData)
    {
        m_dragSlotUI.EndDrag();

        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject == null)
            return;

        if (!draggedObject.TryGetComponent(out ItemSlotUI draggedSlotUI) || draggedSlotUI.SourceContainer == null)
            return;

        ItemSlot draggedItemSlot = draggedSlotUI.CurrentItemSlot;
        if (draggedItemSlot == null || !draggedItemSlot.IsValid())
            return;

        if (draggedItemSlot.Item.Category != m_equipmentSlotUI.EquipmentCategory)
            return;

        m_playerEquipment.EquipItem(draggedSlotUI.SourceContainer, draggedItemSlot);
    }
}
