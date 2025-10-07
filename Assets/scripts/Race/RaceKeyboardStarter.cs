using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
{
    RaceStateTracker _raceStateTracker;

    public void Construct(RaceStateTracker t)
    {
        _raceStateTracker = t;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            _raceStateTracker.LaunchPreparationStart();
        }
    }
}
