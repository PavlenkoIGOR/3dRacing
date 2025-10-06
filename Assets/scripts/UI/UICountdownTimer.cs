using TMPro;
using UnityEngine;

public class UICountdownTimer : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] Timer _countdownTimer;
    [SerializeField] RaceStateTracker _raceStateTracker;

    // Start is called before the first frame update
    void Start()
    {
        _raceStateTracker.PreparationStarted += OnPreparationStarted;
        _raceStateTracker.Started += OnRaceStarted;

        _text.enabled = false;
    }

    private void OnRaceStarted()
    {
        _text.enabled = false;
        enabled = false;
    }

    private void OnPreparationStarted()
    {
        _text.enabled = true;
        enabled = true;
    }

    private void OnDestroy()
    {
        _raceStateTracker.PreparationStarted -= OnPreparationStarted;
        _raceStateTracker.Started -= OnRaceStarted;
    }
    private void Update()
    {
        _text.text = _countdownTimer.Value.ToString("F0");

        if (_text.text == "0")
        {
            _text.text = "GO!";
        }
    }
}
