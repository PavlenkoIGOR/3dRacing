using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    private CarChassis _chassis;

    [SerializeField] private float _maxMotorRorque;
    [SerializeField] private float _maxSteerAngle;
    [SerializeField] private float _maxBrakeTorque;

    public float throttleControl;
    public float steerControl;
    public float brakeControl;

    void Start()
    {
        _chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        _chassis.motorTorque = throttleControl * _maxMotorRorque;
        _chassis.brakeTorque = brakeControl * _maxBrakeTorque;
        _chassis.steerAngle = steerControl * _maxSteerAngle;
    }
}
