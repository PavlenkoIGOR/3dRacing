using UnityEngine;

public class ActivateTrackPoint : TrackPoint
{
    [SerializeField] GameObject _hint;
    // Start is called before the first frame update
    void Start()
    {
        _hint.SetActive(isTarget);
    }

    protected override void OnPassed()
    {
        _hint.SetActive(false);
    }

    protected override void OnAssignAsTarget()
    {
        _hint.SetActive(true);
    }
}
