using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerEquipmentSlotUI : ItemSlotUI
{
    [field: SerializeField]
    public ItemCategory EquipmentCategory { get; private set; }

    [SerializeField]
    private Image m_backgroundItemImage;

    private PlayerEquipment m_playerEquipment;

    [Inject]
    private void Construct(PlayerEquipment playerEquipment)
    {
        m_playerEquipment = playerEquipment;
    }

    private void Awake()
    {
        m_playerEquipment.OnItemSlotAdded += DisplayItem;
        m_playerEquipment.OnItemSlotModified += DisplayItem;

        m_playerEquipment.OnItemSlotRemoved += HideItem;
    }

    private void OnDestroy()
    {
        m_playerEquipment.OnItemSlotAdded -= DisplayItem;
        m_playerEquipment.OnItemSlotModified -= DisplayItem;

        m_playerEquipment.OnItemSlotRemoved -= HideItem;
    }

    private void DisplayItem(ItemSlot itemSlot)
    {
        if (itemSlot.Item.Category != EquipmentCategory)
            return;

        m_backgroundItemImage.gameObject.SetActive(false);
        m_slotImage.gameObject.SetActive(true);

        DisplayItemSlot(m_playerEquipment, itemSlot);
    }

    private void HideItem(ItemSlot itemSlot)
    {
        if (itemSlot.Item.Category != EquipmentCategory)
            return;

        m_backgroundItemImage.gameObject.SetActive(true);
        m_slotImage.gameObject.SetActive(false);
    }
}
