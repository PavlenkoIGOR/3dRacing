using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}

public class TrackPointCircuit : MonoBehaviour
{
    public event UnityAction<TrackPoint> trackPointTriggered;
    public event UnityAction<int> onLapCompleted;
    [SerializeField] TrackType _type;
    [SerializeField] TrackPoint[] _trackPoints;
    int _lapsCompleted = -1;

    void Start()
    {
        onLapCompleted += (TrackPoint) => { 
            Debug.Log("asdasdasd"); 
        };

        for (int i = 0; i < _trackPoints.Length; i++)
        {
            _trackPoints[i].triggered += OnTrackPointTriggered;
        }

        _trackPoints[0].AssignTarget();
    }


    private void OnDestroy()
    {
        for (int i = 0; i < _trackPoints.Length; i++)
        {
            _trackPoints[i].triggered -= OnTrackPointTriggered;
        }
    }

    void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.isTarget == false)
        {
            return;
        }

        trackPoint.Passed();
        trackPoint.nextpoint?.AssignTarget();

        if (trackPoint.isLast == true)
        {
            _lapsCompleted++;
            if (_type == TrackType.Sprint)
            {
                onLapCompleted?.Invoke(_lapsCompleted);
            }

            if (_type == TrackType.Circular)
            {
                if (_lapsCompleted>0)
                {
                    onLapCompleted?.Invoke(_lapsCompleted);
                }
            }
        }
    }
}
