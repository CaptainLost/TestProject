using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform m_inventorySlotParent;

    private PlayerInventory m_playerInventory;
    private ItemSlotUI.Factory m_slotFactory;
    private SignalBus m_signalBus;

    private Dictionary<ItemSlot, ItemSlotUI> m_itemSlotDictionary = new();

    [Inject]
    private void Construct(PlayerInventory playerInventory, ItemSlotUI.Factory slotFactory, SignalBus signalBus)
    {
        m_playerInventory = playerInventory;
        m_slotFactory = slotFactory;
        m_signalBus = signalBus;
    }

    private void Awake()
    {
        m_playerInventory.ItemContainer.OnItemSlotAdded += OnItemSlotAdded;
        m_playerInventory.ItemContainer.OnItemSlotRemoved += OnItemSlotRemoved;

        CreateInitialSlots();
    }

    private void OnDestroy()
    {
        m_playerInventory.ItemContainer.OnItemSlotAdded -= OnItemSlotAdded;
        m_playerInventory.ItemContainer.OnItemSlotRemoved -= OnItemSlotRemoved;
    }

    private void OnEnable()
    {
        m_signalBus.Fire(new GameLockCursor());
    }

    private void OnDisable()
    {
        m_signalBus.Fire(new GameUnlockCursor());
    }

    private void CreateInitialSlots()
    {
        foreach (ItemSlot itemSlot in m_playerInventory.ItemContainer)
        {
            if (!m_itemSlotDictionary.ContainsKey(itemSlot))
            {
                OnItemSlotAdded(itemSlot);
            }
        }
    }

    private void OnItemSlotAdded(ItemSlot itemSlot)
    {
        ItemSlotUI slotUI = m_slotFactory.Create();
        slotUI.transform.SetParent(m_inventorySlotParent);

        slotUI.DisplayItemSlot(m_playerInventory.ItemContainer, itemSlot);

        m_itemSlotDictionary.Add(itemSlot, slotUI);
    }

    // If frequent changes are made, it is worth making a memory pool
    private void OnItemSlotRemoved(ItemSlot itemSlot)
    {
        if (m_itemSlotDictionary.TryGetValue(itemSlot, out ItemSlotUI slotUI))
        {
            Destroy(slotUI.gameObject);
            m_itemSlotDictionary.Remove(itemSlot);

            return;
        }

        // This should never occur
        Debug.LogError($"Failed to remove item ({itemSlot.Item}, {itemSlot.Amount}) from UI");
    }
}
