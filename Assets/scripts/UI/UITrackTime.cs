using TMPro;
using UnityEngine;

public class UITrackTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    [SerializeField] TMP_Text _text;
    RaceTimeTracker _raceTimeTracker;
    RaceStateTracker _raceStateTracker;

    public void Construct(RaceStateTracker t) => _raceStateTracker = t;
    public void Construct(RaceTimeTracker t) => _raceTimeTracker = t;

    void OnRaceStarted()
    {
        _text.enabled = true;
        enabled = true;
    }
    void OnRaceCompleted()
    {
        _text.enabled = false;
        enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;
        _raceStateTracker.Completed += OnRaceCompleted;

        _text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = StringTime.SecondsToTimeString(_raceTimeTracker.currentTime);
    }
    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;
        _raceStateTracker.Completed -= OnRaceCompleted;
    }
}
