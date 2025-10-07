using System;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparation, Countdown, Race, Passed
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    TrackPointCircuit _trackPointCircuit;
    [SerializeField] int _lapsToComplete;
    [SerializeField] Timer _countdownTimer;
    public Timer countdownTimer => _countdownTimer;

    RaceState _state;
    public RaceState State => _state;

    void StartState(RaceState state)
    {
        this._state = state;
    }

    // Start is called before the first frame update
    void Start()
    {

        StartState(RaceState.Preparation);

        _countdownTimer.enabled = false;
        _countdownTimer.Finished += OnCountdownTimerFinished;

        _trackPointCircuit.trackPointTriggered += OnTrackPointTriggered;
        _trackPointCircuit.onLapCompleted += OnLapCompleted;
    }

    private void OnCountdownTimerFinished()
    {
        StartRace();
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (_trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (_trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == _lapsToComplete)
            {
                CompleteRace();
            }
            else
            {
                CompleteLap(lapAmount);
            }
        }
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }
    private void OnDestroy()
    {
        _countdownTimer.Finished -= OnCountdownTimerFinished;

        _trackPointCircuit.trackPointTriggered -= OnTrackPointTriggered;
        _trackPointCircuit.onLapCompleted -= OnLapCompleted;
    }

    public void LaunchPreparationStart()
    {
        if (_state != RaceState.Preparation)
        {
            return;
        }
        StartState(RaceState.Countdown);

        _countdownTimer.enabled = true;

        PreparationStarted?.Invoke();
    }

    public void StartRace()
    {
        if (_state != RaceState.Countdown)
        {
            return;
        }
        StartState(RaceState.Race);

        Started?.Invoke();
    }

    public void CompleteRace()
    {
        if (_state != RaceState.Race)
        {
            return;
        }
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }

    public void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

    public void Construct(TrackPointCircuit trackPointCircuit)
    {
        _trackPointCircuit = trackPointCircuit;
    }
}
