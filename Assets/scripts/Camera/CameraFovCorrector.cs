using UnityEngine;

public class CameraFovCorrector : CarCameraComponent
{
    [SerializeField] float _minFieldOfView;
    [SerializeField] float _maxFieldOfView;

    float _defaultFOV;

    void Start()
    {
        _camera.fieldOfView = _defaultFOV;
    }

    void Update()
    {
        _camera.fieldOfView = Mathf.Lerp(_minFieldOfView, _maxFieldOfView, _car.NormalizeLinearVelocity);
    }
}
