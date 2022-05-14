using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : IInventoryItem
{
    public bool isEquipeed { get ; set; }

    public Type type => GetType();

    public int maxItemsInInventorySlot { get; } 

    public int amount { get ; set; }


    public Apple(int maxItemsInInventorySlot)
    {
        this.maxItemsInInventorySlot = maxItemsInInventorySlot;
    }
    public IInventoryItem Clone()
    {
        return new Apple(maxItemsInInventorySlot)
        {
            amount = this.amount
        };
    }

}
