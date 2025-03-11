using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerInventoryDropHandler : MonoBehaviour, IDropHandler
{
    private DraggedItemUI m_dragSlotUI;
    private PlayerInventory m_playerInventory;

    [Inject]
    private void Construct(DraggedItemUI dragSlotUI, PlayerInventory playerInventory)
    {
        m_dragSlotUI = dragSlotUI;
        m_playerInventory = playerInventory;
    }

    public void OnDrop(PointerEventData eventData)
    {
        m_dragSlotUI.EndDrag();

        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject == null)
            return;

        if (!draggedObject.TryGetComponent(out PlayerEquipmentSlotUI equipmentSlotUI) || equipmentSlotUI.SourceContainer == null)
            return;

        ItemSlot draggedItemSlot = equipmentSlotUI.CurrentItemSlot;
        if (draggedItemSlot == null || !draggedItemSlot.IsValid())
            return;

        equipmentSlotUI.SourceContainer.TransferItem(m_playerInventory.ItemContainer, draggedItemSlot.Item, draggedItemSlot.Amount);
    }
}
