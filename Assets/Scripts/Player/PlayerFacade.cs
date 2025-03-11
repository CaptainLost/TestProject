public class PlayerFacade : IPlayer
{
    public PlayerInventory PlayerInventory { get; private set; }
    public PlayerEquipment PlayerEquipment { get; private set; }

    public PlayerFacade(PlayerInventory playerInventory)
    {
        PlayerInventory = playerInventory;
    }
}
