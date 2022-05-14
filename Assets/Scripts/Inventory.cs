using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;
    public int capacity { get; set; }

    public bool isFull => _slots.All(slot => slot.isFull);

    private List<IInventorySlot> _slots;

    public Inventory(int capacity)
    {
        this.capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);
        for (int i = 0; i <capacity; i++)
        {
            _slots.Add(new InventorySlot());
        }
    }
    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();
        foreach (var slot  in _slots)
        {
            if (!slot.isEmpty)
            {
                allItems.Add(slot.item);
            }
        }
        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();

        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

        foreach (var slot in slotsOfType)
        {
            allItemsOfType.Add(slot.item);
        }
        return allItemsOfType.ToArray();


    }

    public IInventoryItem[] GetEquippedItems()
    {
        var requiredSlots = _slots.FindAll(slot => slot.item.isEquipeed);
        var equippedItems = new List<IInventoryItem>();
        foreach (var slot in requiredSlots)
        {
            equippedItems.Add(slot.item);
        }
        return equippedItems.ToArray();
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }

    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var allItems = GetAllItems(itemType);
        foreach (var item in allItems)
        {
            amount += item.amount;
        }
        return amount;
    }

    public bool HasItem(Type itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);
        return item != null;
    }

    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithitem = GetAllSlots(itemType);
        if (slotsWithitem.Length == 0)
            return;
        var amountToRemove = amount;

        var count = slotsWithitem.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            var slot = slotsWithitem[i];
            if (slot.amount >= amountToRemove)
            {
                slot.item.amount -= amountToRemove;
                if (slot.amount <= 0)
                    slot.Clear();
                Debug.Log($"Item removed . itemType: {itemType}, amount: {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                break;
            }
            var amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            Debug.Log($"Item removed . itemType: {itemType}, amount: {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);




        }

    }

    public  IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }
    public  IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);
        if (slotWithSameItemButNotEmpty != null)
            return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);

        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);

        return false;

    }
    private bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.amount <= item.maxItemsInInventorySlot;
        var amountToAdd = fits ? item.amount : item.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.amount = amountToAdd;
        if (slot.isEmpty)
            slot.SetItem(clonedItem);
        else
            slot.item.amount += amountToAdd;
        Debug.Log($"Item added to inventory. itemType: {item.type}, amount: {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);

        if (amountLeft <= 0)
            return true;

        item.amount = amountLeft;

        return TryToAdd(sender, item);

    }

}
