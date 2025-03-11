using System;
using Zenject;

public class PlayerStatistics : IInitializable, IDisposable
{
    public PlayerAttributes Attributes;

    private readonly PlayerSettingsSO m_playerSettings;
    private readonly PlayerEquipment m_playerEquipment;
    private readonly SignalBus m_signalBus;

    public PlayerStatistics(PlayerSettingsSO playerSettings, PlayerEquipment playerEquipment, SignalBus signalBus)
    {
        m_playerSettings = playerSettings;
        m_playerEquipment = playerEquipment;
        m_signalBus = signalBus;
    }

    public void Initialize()
    {
        m_playerEquipment.OnItemSlotAdded += OnPlayerEquipmentChanged;
        m_playerEquipment.OnItemSlotModified += OnPlayerEquipmentChanged;
        m_playerEquipment.OnItemSlotRemoved += OnPlayerEquipmentChanged;

        RecalculateStatistics();
    }

    public void Dispose()
    {
        m_playerEquipment.OnItemSlotAdded -= OnPlayerEquipmentChanged;
        m_playerEquipment.OnItemSlotModified -= OnPlayerEquipmentChanged;
        m_playerEquipment.OnItemSlotRemoved -= OnPlayerEquipmentChanged;
    }

    public void OnPlayerEquipmentChanged(ItemSlot itemSlot)
    {
        RecalculateStatistics();
    }

    public void RecalculateStatistics()
    {
        Attributes = m_playerSettings.BaseAttributes;

        // Move speed
        foreach (ItemSlot inventorySlot in m_playerEquipment)
        {
            Attributes.MoveSpeedModifier += inventorySlot.Item.MoveSpeedModifier;
        }
        Attributes.MoveSpeed = Attributes.BaseMoveSpeed * (Attributes.MoveSpeedModifier + 1f);

        // Damage
        foreach (ItemSlot inventorySlot in m_playerEquipment)
        {
            Attributes.AdditionalDamage += inventorySlot.Item.AdditionalDamage;
        }
        Attributes.Damage = Attributes.BaseDamage + Attributes.AdditionalDamage;

        m_signalBus.Fire(new PlayerStatisticsCalculatedSignal(Attributes));
    }
}
