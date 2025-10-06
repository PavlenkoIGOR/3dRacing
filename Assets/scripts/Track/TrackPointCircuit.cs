using System;
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
    TrackPoint[] _trackPoints;
    int _lapsCompleted = -1;

    public TrackType Type { get => _type; set => _type = value; }

    private void Awake()
    {
        BuildCrcuit();
    }
    void Start()
    {


        for (int i = 0; i < _trackPoints.Length; i++)
        {
            _trackPoints[i].triggered += OnTrackPointTriggered;
        }

        _trackPoints[0].AssignAsTarget();
    }

    [ContextMenu(nameof(BuildCrcuit))]
    private void BuildCrcuit()
    {
        _trackPoints = TrackCircuitBuilder.Build(transform, _type);
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
        trackPoint.nextpoint?.AssignAsTarget();

        trackPointTriggered?.Invoke(trackPoint);

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
