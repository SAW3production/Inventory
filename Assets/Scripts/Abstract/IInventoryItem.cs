using System;

public interface IInventoryItem
{
    bool isEquipeed { get; set; }
    Type type { get; }
    int maxItemsInInventorySlot { get; }
    int amount { get; set; }

    IInventoryItem Clone();





}
