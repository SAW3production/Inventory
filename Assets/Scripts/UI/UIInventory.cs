using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public Inventory inventory { get; private set; }

    private void Awake()
    {
        inventory = new Inventory(18);

    }

}
