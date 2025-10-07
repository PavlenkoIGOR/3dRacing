using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] Car _car;
    [SerializeField] Camera _camera;
    [SerializeField] CameraFollow _follower;
    [SerializeField] CameraShaker _shaker;
    [SerializeField] CameraFovCorrector _fovCorrector;
    [SerializeField] CameraPathFollower _pathFollower;

    RaceStateTracker _raceStatetracker;
    void Awake()
    {
        _follower.SetProperties(_car, _camera);
        _shaker.SetProperties(_car, _camera);
        _fovCorrector.SetProperties(_car, _camera);
    }

    void OnPreparationStarted()
    {
        _follower.enabled = true;
        _pathFollower.enabled = false;
    }

    void OnCompleted()
    {
        _pathFollower.enabled = true;
        _pathFollower.StartMoveToNearestPoint();
        _pathFollower.SetLookTarget(_car.transform);

        _follower.enabled = false;
    }

    private void Start()
    {
        _raceStatetracker.PreparationStarted += OnPreparationStarted;
        _raceStatetracker.Completed += OnCompleted;

        _follower.enabled = false;
        _pathFollower.enabled = true;
    }

    private void OnDestroy()
    {
        _raceStatetracker.PreparationStarted -= OnPreparationStarted;
        _raceStatetracker.Completed -= OnCompleted;
    }

    public void Construct(RaceStateTracker t)
    {
        _raceStatetracker = t;
    }
}
