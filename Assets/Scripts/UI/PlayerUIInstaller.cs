using UnityEngine;
using Zenject;

public class PlayerUIInstaller : MonoInstaller
{
    [SerializeField]
    private ItemSlotUI m_inventorySlotUIPrefab;
    [SerializeField]
    private DraggedItemUI m_draggedSlotUI;
    [SerializeField]
    private TooltipController m_tooltipController;

    public override void InstallBindings()
    {
        Container.BindFactory<ItemSlotUI, ItemSlotUI.Factory>()
            .FromComponentInNewPrefab(m_inventorySlotUIPrefab)
            .AsSingle();

        Container.Bind<DraggedItemUI>()
            .FromInstance(m_draggedSlotUI)
            .AsSingle();

        Container.Bind<TooltipController>()
            .FromInstance(m_tooltipController)
            .AsSingle();
    }
}
