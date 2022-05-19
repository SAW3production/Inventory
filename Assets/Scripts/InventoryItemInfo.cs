
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create New Item Info")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _discription;
    [SerializeField] private int _maxItemsInInventorySlot;
    [SerializeField] private Sprite _spriteIcon;

    public string id => _id;

    public string title => _title;

    public string discription => _discription;

    public int maxItemsInInventorySlot => _maxItemsInInventorySlot;

    public Sprite spriteIcon => _spriteIcon;

}
