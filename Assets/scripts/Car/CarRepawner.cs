using System;
using UnityEngine;

public class CarRepawner : MonoBehaviour,IDependency<RaceStateTracker>, IDependency<Car>, IDependency<CarInputControl>
{
    [SerializeField] float _respawnHeight;
    //[SerializeField] float _respawnAngle;
    TrackPoint _respawnTrackPoint;
    RaceStateTracker _raceStateTracker;
    Car _car;
    CarInputControl _carControl;
    public void Construct(RaceStateTracker t) => _raceStateTracker = t;
    public void Construct(Car t) => _car = t;
    public void Construct(CarInputControl t) => _carControl = t;

    // Start is called before the first frame update
    void Start()
    {
        _raceStateTracker.TrackPointPassed += OnTrackPointPassed;
    }

    private void OnTrackPointPassed(TrackPoint point)
    {
        _respawnTrackPoint = point;
    }

    private void OnDestroy()
    {
        _raceStateTracker.TrackPointPassed -= OnTrackPointPassed;
    }

    public void Respawn()
    {
        if (_respawnTrackPoint == null)
        {
            return;
        }

        if (_raceStateTracker.State != RaceState.Race)
        {
            return;
        }


        Vector3 respawnPosition = _respawnTrackPoint.transform.position + _respawnTrackPoint.transform.up * _respawnHeight;

        // Создаем ориентацию так, чтобы ось Y автомобиля совпадала с направлением +Z _respawnTrackPoint
        Quaternion rotation = Quaternion.LookRotation(_respawnTrackPoint.transform.up, _respawnTrackPoint.transform.forward);

        _car.Respawn(respawnPosition, rotation);

        _carControl.ControlReset();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
}
