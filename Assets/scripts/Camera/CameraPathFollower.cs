using UnityEngine;

public class CameraPathFollower : CarCameraComponent
{
    [SerializeField] Transform _path;
    [SerializeField] Transform _lookTarget;
    [SerializeField] float _movementSpeed;

    Vector3[] _points;
    int _pointIndex;
    // Start is called before the first frame update
    void Start()
    {
        _points = new Vector3[_path.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _path.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_pointIndex], _movementSpeed * Time.deltaTime);

        if (transform.position == _points[_pointIndex])
        {
            if (_pointIndex == _points.Length- 1)
            {
                _pointIndex = 0;
            }
            else
            {
                _pointIndex++;
            }
        }

        transform.LookAt(_lookTarget);
    }

    public void StartMoveToNearestPoint()
    {
        float minDist = float.MaxValue;

        for (int i = 0; i < _points.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, _points[i]);
            if (dist < minDist)
            {
                minDist = dist;
                _pointIndex = i;
            }
        }
    }

    public void SetLookTarget(Transform target)
    {
        _lookTarget = target;
    }
}
