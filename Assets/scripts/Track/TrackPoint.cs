using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public event UnityAction<TrackPoint> triggered;

    public TrackPoint nextpoint;
    public bool isFirst;
    public bool isLast;

    protected bool _isTarget;
    public bool isTarget => _isTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null)
        {
            return;            
        }
        triggered?.Invoke(this);
    }


    public void Passed()
    {
        _isTarget = false;
    }

    public void AssignAsTarget()
    {
        _isTarget = true;
    }

    public void Reset()
    {
        nextpoint = null;
        isFirst = false;
        isLast = false;
    }

}
