using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableBttn : UI_Button
{
    [SerializeField] Image _selectImg;
    public UnityEvent onSelect;
    public UnityEvent onUnSelect;

    public override void SetFocus()
    {
        base.SetFocus();
        _selectImg.enabled = true;
        onSelect?.Invoke();
    }

    public override void SetUnFocus()
    {
        base.SetUnFocus();
        _selectImg.enabled = false;
        onUnSelect?.Invoke();
    }
}
