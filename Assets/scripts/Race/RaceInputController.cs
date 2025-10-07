using UnityEngine;

public class RaceInputController : MonoBehaviour,IDependency<RaceStateTracker>, IDependency<CarInputControl>
{
    CarInputControl _carControl;
    RaceStateTracker _raceStateTracker;

    // Start is called before the first frame update
    void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;
        _raceStateTracker.Completed += OnRaceFinished;

        _carControl.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRaceStarted()
    {
        _carControl.enabled = true;
    }

    private void OnRaceFinished()
    {
        _carControl.enabled = false;

        _carControl.Stop();
    }

    private void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;
        _raceStateTracker.Completed -= OnRaceFinished;
    }

    public void Construct(RaceStateTracker t)
    {
        _raceStateTracker = t;
    }

    public void Construct(CarInputControl t)
    {
        _carControl = t;
    }
}
