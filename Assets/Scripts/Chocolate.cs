using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : IInventoryItem
{
    public Type type => GetType();

    public IInventoryItemInfo info { get; }

    public IInventoryItemState state { get; }

    public Chocolate(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IInventoryItem Clone()
    {
        var clonedChocolate = new Chocolate(info);
        clonedChocolate.state.amount = state.amount;
        return clonedChocolate;
    }

}
