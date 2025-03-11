using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ItemSlotDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private ItemSlotUI m_itemSlotUI;

    private DraggedItemUI m_draggedUI;

    [Inject]
    private void Construct(DraggedItemUI draggedUI)
    {
        m_draggedUI = draggedUI;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ItemSlot draggedSlot = m_itemSlotUI.CurrentItemSlot;

        if (!draggedSlot.IsValid())
            return;

        m_draggedUI.StartDrag(m_itemSlotUI.transform.position, draggedSlot.Item.Sprite);
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_draggedUI.DragUpdate(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_draggedUI.EndDrag();
    }
}
