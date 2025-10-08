using System;
using TMPro;
using UnityEngine;

public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
{
    [SerializeField] GameObject _goldRecordObj;
    [SerializeField] GameObject _playerRecordObj;
    [SerializeField] TMP_Text _goldRecordTime;
    [SerializeField] TMP_Text _playerRecordTime;
    [SerializeField] TMP_Text _recordLabel;
    

    private RaceResultTime _raceResultTime;
    public void Construct(RaceResultTime t) => _raceResultTime = t;

    RaceStateTracker _raceStateTracker;
    public void Construct(RaceStateTracker t) => _raceStateTracker = t;

    void Start()
    {
        _raceStateTracker.Started += OnRaceStart;
        _raceStateTracker.Completed += OnRaceCompleted;

        _goldRecordObj.SetActive(false);
        _playerRecordObj.SetActive(false);
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStart;
        _raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStart()
    {
        if (_raceResultTime.playerRecordTime > _raceResultTime.goldTime || _raceResultTime.recordWasSet == false)
        {
            _goldRecordObj.SetActive(true);
            _goldRecordTime.text = StringTime.SecondsToTimeString(_raceResultTime.goldTime);
        }

        if (_raceResultTime.recordWasSet)
        {
            _playerRecordObj.SetActive(true);
            _playerRecordTime.text = StringTime.SecondsToTimeString(_raceResultTime.playerRecordTime);
        }
    }

    private void OnRaceCompleted()
    {
        _goldRecordObj.SetActive(false);
        _playerRecordObj.SetActive(false);
    }
}
