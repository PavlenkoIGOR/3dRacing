using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRaceButton : UISelectableBttn
{
    [SerializeField] RaceInfo _raceInfo;

    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _title;

    public void ApplyProperty(RaceInfo prop)
    {
        if (prop == null)
        {
            return;
        }

        _raceInfo = prop;
        _icon.sprite = _raceInfo.sprite;
        _title.text = _raceInfo.title;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (_raceInfo == null)
        {
            return;
        }
        SceneManager.LoadScene(_raceInfo.sceneName);
    }

    private void Start()
    {
        ApplyProperty(_raceInfo);
    }
}
