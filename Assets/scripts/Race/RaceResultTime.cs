using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    public const string SaveMark = "_player_best_time";
    public event UnityAction resultUpdate;
    [SerializeField] float _goldTime;
    float _playerRecordTime;
    float _currentTime;

    public float goldTime => _goldTime;
    public float playerRecordTime => _playerRecordTime;
    public float currentTime => _currentTime;
    public bool recordWasSet => _playerRecordTime != 0;

    private RaceTimeTracker _raceTimeTracker;
    public void Construct(RaceTimeTracker t) => _raceTimeTracker = t;

    RaceStateTracker _raceStateTracker;
    public void Construct(RaceStateTracker t) => _raceStateTracker = t;


    void Awake()
    {
        Load();
    }
    void Start()
    {
        _raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        _raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {
        float absoluteRecord = GetAbsoluteRecord();

        if (_raceTimeTracker.currentTime < absoluteRecord || _playerRecordTime == 0)
        {
            _playerRecordTime = _raceTimeTracker.currentTime;

            Save();
        }
        _currentTime = _raceTimeTracker.currentTime;
        resultUpdate?.Invoke();
    }
    public float GetAbsoluteRecord()
    {
        if (_playerRecordTime < _goldTime && _playerRecordTime != 0 )
        {
            return _playerRecordTime;
        }
        else
        {
            return _goldTime;
        }
    }

    private void Load()
    {
        _playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, +playerRecordTime);
    }
}
