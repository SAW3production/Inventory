using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : UIItem
{
    [SerializeField] private Image _imageIcon;
    [SerializeField] private Text _textAmount;

    public IInventoryItem item { get; private set; }
    
    public void Refresh(IInventorySlot slot)
    {
        if (slot.isEmpty)
        {
            Cleanup();
            return;
        }
        item = slot.item;

        _imageIcon.sprite = item.info.spriteIcon;

        var textAmountEnabled = slot.amount > 1;
        _textAmount.gameObject.SetActive(textAmountEnabled);
        if (textAmountEnabled)
            _textAmount.text = $"x{slot.amount.ToString()}";

    }

    public void Cleanup()
    {
        _textAmount.gameObject.SetActive(false);
        _imageIcon.gameObject.SetActive(false);
    }
}