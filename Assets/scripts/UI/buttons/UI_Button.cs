using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] bool _isInteractable;

    bool _focus = false;
    public bool focus => _focus;

    public UnityEvent OnClick;

    public event UnityAction<UI_Button> pointerEnter;
    public event UnityAction<UI_Button> pointerExit;
    public event UnityAction<UI_Button> pointerClick;

    public virtual void SetFocus()
    {
        if (_isInteractable == false)
        {
            return;
        }
        _focus = true;
    }

    public virtual void SetUnFocus()
    {
        if (_isInteractable == false)
        {
            return;
        }
        _focus = false;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isInteractable == false)
        {
            return;
        }

        pointerEnter?.Invoke(this);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isInteractable == false)
        {
            return;
        }
        pointerExit?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isInteractable == false)
        {
            return;
        }
        pointerClick?.Invoke(this);
        OnClick?.Invoke();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
