using System;

[Serializable]
public class InventoryItemState : IInventoryItemState
{
    private int itemAmount;
    private bool isItemEquipped;
    public int amount { get => itemAmount ; set => itemAmount = value; }
    public bool isEquipped { get =>isItemEquipped ; set => isItemEquipped = value ; }

    public InventoryItemState()
    {
        itemAmount = 0;
        isItemEquipped = false;
    }
}
