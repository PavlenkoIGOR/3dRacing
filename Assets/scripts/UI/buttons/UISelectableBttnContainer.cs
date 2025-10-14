using UnityEngine;

public class UISelectableBttnContainer : MonoBehaviour
{
    [SerializeField] Transform _bttnsContainer;

    public bool interactable = true;
    public void SetInteractable(bool interact) => interactable = interact;

    UISelectableBttn[] _bttns;
    int _selectBttnIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _bttns = _bttnsContainer.GetComponentsInChildren<UISelectableBttn>();
        if (_bttns == null)
        {
            Debug.LogError("bttns list is empty");
        }

        for (int i = 0; i < _bttns.Length; i++)
        {
            _bttns[i].pointerEnter += OnPointerEnter;
            
        }

        if (interactable == false)
        {
            return;
        }

        _bttns[_selectBttnIndex].SetFocus();
    }

    public void OnPointerEnter(UI_Button uI_Button)
    {
        SelectBttn(uI_Button);
        Debug.Log("OnPointerEnter");
    }

    private void SelectBttn(UI_Button uI_Button)
    {
        if (interactable == false)
        {
            return;
        }

        _bttns[_selectBttnIndex].SetUnFocus();

        for (int i = 0; i < _bttns.Length; i++)
        {
            if (uI_Button == _bttns[i])
            {
                _selectBttnIndex = i;
                uI_Button.SetFocus();
                break;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _bttns.Length; i++)
        {
            _bttns[i].pointerEnter -= OnPointerEnter;
        }
    }

    public void SelectNext()
    {

    }

    public void SelectPrev()
    {

    }
}
