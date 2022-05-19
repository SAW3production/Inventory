using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInventoryItemInfo
{
    string id { get; }
    string title { get; }
    string discription { get; }
    int maxItemsInInventorySlot { get; }
    Sprite spriteIcon { get; }

}
