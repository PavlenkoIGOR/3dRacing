using System;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider _leftWheelCollider;
    [SerializeField] private WheelCollider _rightWheelCollider;

    [SerializeField] private Transform _leftWheelMesh;
    [SerializeField] private Transform _rightWheelMesh;

    [SerializeField] private float _additionalWheelDownForce;//доп сила вниз дл€ колЄс
    [Tooltip("antikren")][SerializeField] private float _antiRolForce;//антикрен и антиопрокидование
    [SerializeField] private float _wheelBaseWidth; //ширина колЄсной базы
    WheelHit _leftWheelHit;
    WheelHit _rightWheelHit;

    [SerializeField] private bool _isMotor;//приводное
    [SerializeField] private bool _isSteer;//поворотное

    [SerializeField] private float _baseForwardStiffness = 1.5f;
    [SerializeField] private float _stabilityForwardFactor = 1.0f;
    [SerializeField] private float _baseSlidewaysStiffness = 2.0f;
    [SerializeField] private float _stabilitySidewaysFactor = 1.0f;
    public bool IsMotor => _isMotor;
    public bool IsSteer => _isSteer;

    public void Update()
    {
        UpdateWheelHits();

        SyncMeshTransform();

        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStiffness();
    }

    private void UpdateWheelHits()
    {
        _leftWheelCollider.GetGroundHit(out _leftWheelHit);
        _rightWheelCollider.GetGroundHit(out _rightWheelHit);
    }

    /// <summary>
    /// ѕрименение силы трени€ дл€ колЄс
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void CorrectStiffness()
    {
        //кривые пр€молинейного трени€
        WheelFrictionCurve leftForward = _leftWheelCollider.forwardFriction;
        WheelFrictionCurve rightForward = _rightWheelCollider.forwardFriction;

        //кривые трени€ при боковом смещении
        WheelFrictionCurve leftSideway = _leftWheelCollider.sidewaysFriction;
        WheelFrictionCurve rightSideway = _rightWheelCollider.sidewaysFriction;

        leftForward.stiffness = _baseForwardStiffness + Mathf.Abs(_leftWheelHit.forwardSlip) * _stabilityForwardFactor;
        rightForward.stiffness = _baseForwardStiffness + Mathf.Abs(_rightWheelHit.forwardSlip) * _stabilityForwardFactor;

        leftSideway.stiffness = _baseSlidewaysStiffness + Mathf.Abs(_leftWheelHit.sidewaysSlip) * _stabilitySidewaysFactor;
        rightSideway.stiffness = _baseSlidewaysStiffness + Mathf.Abs(_rightWheelHit.sidewaysSlip) * _stabilitySidewaysFactor;

        _leftWheelCollider.forwardFriction = leftForward;
        _rightWheelCollider.forwardFriction = rightForward;
        _leftWheelCollider.sidewaysFriction = leftSideway;
        _rightWheelCollider.sidewaysFriction = rightSideway;

        if (_leftWheelCollider.isGrounded)
        {
            _leftWheelCollider.attachedRigidbody.AddForceAtPosition(_leftWheelHit.normal * -_additionalWheelDownForce * _leftWheelCollider.attachedRigidbody.velocity.magnitude,
                _leftWheelCollider.transform.position);
        }

        if (_rightWheelCollider.isGrounded)
        {
            _rightWheelCollider.attachedRigidbody.AddForceAtPosition(_rightWheelHit.normal * -_additionalWheelDownForce * _rightWheelCollider.attachedRigidbody.velocity.magnitude,
                _rightWheelCollider.transform.position);
        }
    }

    /// <summary>
    /// ѕрижимна€ сила дл€ колЄс
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ApplyDownForce()
    {
    }

    /// <summary>
    /// —табилизатор поперечной устойчивости
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if (_leftWheelCollider.isGrounded)
        {
            travelL = (-_leftWheelCollider.transform.InverseTransformPoint(_leftWheelHit.point).y - _leftWheelCollider.radius) / _leftWheelCollider.suspensionDistance;
        }
        if (_rightWheelCollider.isGrounded)
        {
            travelR = (-_rightWheelCollider.transform.InverseTransformPoint(_rightWheelHit.point).y - _rightWheelCollider.radius) / _rightWheelCollider.suspensionDistance;
        }

        float forceDir = (travelL - travelR);

        if (_leftWheelCollider.isGrounded)
        {
            _leftWheelCollider.attachedRigidbody.AddForceAtPosition(_leftWheelCollider.transform.up * -forceDir * _antiRolForce, _leftWheelCollider.transform.position);
        }

        if (_rightWheelCollider.isGrounded)
        {
            _rightWheelCollider.attachedRigidbody.AddForceAtPosition(_rightWheelCollider.transform.up * forceDir * _antiRolForce, _rightWheelCollider.transform.position);
        }
    }

    /// <summary>
    /// установка частоты обновлени€ колесного коллайдера колеса
    /// </summary>
    /// <param name="speedThreshold"></param>
    /// <param name="speedBelowThreshold"></param>
    /// <param name="stepAboveThreshold"></param>
    public void ConfigureVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int stepAboveThreshold)
    {
        _leftWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepAboveThreshold);
        _rightWheelCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepAboveThreshold);
    }

    /// <summary>
    /// ѕоворот колЄс
    /// </summary>
    /// <param name="steerAngle"></param>
    /// <param name="wheelBaseLength">длина колЄсной базы</param>
    public void ApplySteeringAngle(float steerAngle, float wheelBaseLength)
    {
        if (_isSteer == false)//если наша ось не поворачиваема€, то выходим из метода
        {
            return;
        }
        #region Ackerman angle
        //центр поворота
        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
        float angleSign = Mathf.Sign(steerAngle);

        if (steerAngle > 0)
        {
            _leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (_wheelBaseWidth * 0.5f))) * angleSign;
            _rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (_wheelBaseWidth * 0.5f))) * angleSign;
        }
        else if (steerAngle < 0)
        {
            _leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (_wheelBaseWidth * 0.5f))) * angleSign;
            _rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (_wheelBaseWidth * 0.5f))) * angleSign;
        }
        else
        {
            _leftWheelCollider.steerAngle = 0;
            _rightWheelCollider.steerAngle = 0;
        }
        #endregion



    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (_isMotor == false)
        {
            return;
        }

        _leftWheelCollider.motorTorque = motorTorque;
        _rightWheelCollider.motorTorque = motorTorque;
    }

    /// <summary>
    /// метод, возвращающий скорость колЄс, (дл€ логики плавного торможени€)
    /// </summary>
    public float GetAverageRPM()
    {
        return (_leftWheelCollider.rpm + _rightWheelCollider.rpm) / 0.5f;
    }
    public float GetRadius()
    {
        return _leftWheelCollider.radius;
    }

    public void ApplyBreakTorque(float brakeTorque)
    {
        _leftWheelCollider.brakeTorque = brakeTorque;
        _rightWheelCollider.brakeTorque = brakeTorque;
    }
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(_leftWheelCollider, _leftWheelMesh);
        UpdateWheelTransform(_rightWheelCollider, _rightWheelMesh);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wTransform.position = pos;
        wTransform.rotation = rot;
    }
}
