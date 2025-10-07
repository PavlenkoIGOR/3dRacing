using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
{
    RaceStateTracker _raceStateTracker;

    float _currentTime;
    public float currentTime => _currentTime;
    public void Construct(RaceStateTracker t) => _raceStateTracker = t;

    void OnRaceCompleted()
    {
        enabled = false;
    }
    void OnRaceStarted()
    {
        enabled = true;
        _currentTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        _raceStateTracker.Started += OnRaceStarted;
        _raceStateTracker.Completed += OnRaceCompleted;
    }

    void OnDestroy()
    {
        _raceStateTracker.Started -= OnRaceStarted;
        _raceStateTracker.Completed -= OnRaceCompleted;
    }
    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
    }
}
