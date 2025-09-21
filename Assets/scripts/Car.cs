using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    private CarChassis _chassis;

    [SerializeField] private float _maxSteerAngle;
    [SerializeField] private float _maxBrakeTorque;

    [SerializeField] private float _maxMotorTorque;
    [SerializeField] private AnimationCurve _engineTorqueCurve;
    [SerializeField] private float _maxSpeed;
    public float LinearVelocity => _chassis.linearVelocity;
    public float WheelSpeed => _chassis.GetWheelSpeed();
    public float MaxSpeed => _maxSpeed;

    [Header("debug")]
    public float throttleControl;
    public float steerControl;
    public float brakeControl;
    public float linVelocity;

    void Start()
    {
        _chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        linVelocity = LinearVelocity;

        float engineTorque = _engineTorqueCurve.Evaluate(LinearVelocity / _maxSpeed) * _maxMotorTorque;

        if (LinearVelocity >= _maxSpeed)
        {
            engineTorque = 0;
        }
        _chassis.motorTorque = throttleControl * engineTorque;
        _chassis.brakeTorque = brakeControl * _maxBrakeTorque;
        _chassis.steerAngle = steerControl * _maxSteerAngle;
    }
}
