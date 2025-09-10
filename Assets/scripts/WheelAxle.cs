using System;
using UnityEngine;

[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider _leftWheelCollider;
    [SerializeField] private WheelCollider _rightWheelCollider;

    [SerializeField] private Transform _leftWheelMesh;
    [SerializeField] private Transform _rightWheelMesh;

    [SerializeField] private bool _isMotor;//приводное
    [SerializeField] private bool _isSteer;//поворотное

    public void Update()
    {
        SyncMeshTransform();
    }

    public void ApplySteeringAngle(float steerAngle)
    {
        if (_isSteer == false)//если наша ось не поворачиваемая, то выходим из метода
        {
            return; 
        }

        _leftWheelCollider.steerAngle = steerAngle;
        _rightWheelCollider.steerAngle = steerAngle;

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

    public void ApplyBreakTorque(float brakeTorque)
    {
        _leftWheelCollider.brakeTorque = brakeTorque;
        _rightWheelCollider.motorTorque = brakeTorque;
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
