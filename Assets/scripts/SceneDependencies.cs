using UnityEngine;

public interface IDependency<T>
{
    public void Construct(T t);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] TrackPointCircuit _trackPointCircuit;
    [SerializeField] RaceStateTracker _raceStateTracker;
    [SerializeField] CarInputControl _carInputControl;
    [SerializeField] Car _car;
    [SerializeField] CarCameraController _carCameraController;
    [SerializeField] RaceTimeTracker _raceTimeTracker;

    void Bind(MonoBehaviour mono)
    {
        if (mono is IDependency<TrackPointCircuit>)
        {
            (mono as IDependency<TrackPointCircuit>).Construct(_trackPointCircuit);
        }

        if (mono is IDependency<RaceStateTracker>)
        {
            (mono as IDependency<RaceStateTracker>).Construct(_raceStateTracker);
        }

        if (mono is IDependency<CarInputControl>)
        {
            (mono as IDependency<CarInputControl>).Construct(_carInputControl);
        }

        if (mono is IDependency<Car>)
        {
            (mono as IDependency<Car>).Construct(_car);
        }

        if (mono is IDependency<CarCameraController>)
        {
            (mono as IDependency<CarCameraController>).Construct(_carCameraController);
        }

        if (mono is IDependency<RaceTimeTracker>)
        {
            (mono as IDependency<RaceTimeTracker>).Construct(_raceTimeTracker);
        }
    }
    private void Awake()
    {
        MonoBehaviour[] allMonoBeh = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allMonoBeh.Length; i++)
        {
            Bind(allMonoBeh[i]);

        }
    }
}
