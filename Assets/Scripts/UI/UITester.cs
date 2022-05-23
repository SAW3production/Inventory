using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITester 
{
    private InventoryItemInfo _appleInfo;
    private InventoryItemInfo _chocolateInfo;
    private UIInventorySlot[] _uiSlots;

    public Inventory inventory { get; }

    public UITester(InventoryItemInfo appleInfo,InventoryItemInfo chocolateInfo, UIInventorySlot[] uiSlots )
    {
        _appleInfo = appleInfo;
        _chocolateInfo = chocolateInfo;
        _uiSlots = uiSlots;

        inventory = new Inventory(18);

        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }
    public void FillSlots(int filledSlots)
    {
        var allSlots = inventory.GetAllSlots();
        var availableSlots = new List<IInventorySlot>(allSlots);

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomApplesIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);

            filledSlot = AddRandomChocolatesIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }
        SetupInventoryUI(inventory);
    }
    private IInventorySlot AddRandomApplesIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var apple = new Apple(_appleInfo);
        apple.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, apple);
        return rSlot;
    }
    private IInventorySlot AddRandomChocolatesIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var chocolate = new Chocolate(_chocolateInfo);
        chocolate.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, chocolate);
        return rSlot;
    }
    private void SetupInventoryUI(Inventory inventory)
    {
        var allslots = inventory.GetAllSlots();
        var allslotsCount = allslots.Length;
        for (int i = 0; i < allslotsCount; i++)
        {
            var slot = allslots[i];
            var uislot = _uiSlots[i];
            uislot.SetSlot(slot);
            uislot.Refresh();
        }
    }

    private void OnInventoryStateChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
}
