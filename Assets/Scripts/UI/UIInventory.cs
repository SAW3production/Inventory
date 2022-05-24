using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] InventoryItemInfo _appleInfo;
    [SerializeField] InventoryItemInfo _chocolateInfo;
    public Inventory inventory => tester.inventory;
    private UITester tester;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        tester = new UITester(_appleInfo,_chocolateInfo,uiSlots);
        tester.FillSlots(3);
        

    }

}
