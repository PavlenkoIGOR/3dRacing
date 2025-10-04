using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] _wheelAxles;
    [SerializeField] private float _wheelBaseLength;
    [SerializeField] private Transform _centerOfMass;

    [Header("DownForce")]
    [SerializeField] private float _downForceMin;
    [SerializeField] private float _downForceMax;
    [SerializeField] private float _downForceFactor;

    [Header("AngularDrag")]
    [SerializeField] private float _angularDragMin;
    [SerializeField] private float _angularDragMax;
    [SerializeField] private float _angularDragFactor;

    public float linearVelocity => _rigidbody.velocity.magnitude * 3.6f;

    [Header("debug")]
    public float motorTorque;
    public float brakeTorque;
    public float steerAngle;

    private Rigidbody _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_centerOfMass)
        {
            _rigidbody.centerOfMass = _centerOfMass.localPosition;
        }

        for (int i = 0; i < _wheelAxles.Length; i++)
        {
            _wheelAxles[i].ConfigureVehicleSubsteps(50,50,50);
        }
    }
    private void FixedUpdate()
    {
        UpdateAngularDrag();
        UpdateDownForce();
        UpdateWheelAxles();
    }

    /// <summary>
    /// воозвращает среднюю частоту вращения колёс
    /// </summary>
    /// <returns></returns>
    public float GetAverageRPM()
    {
        float sum = 0;

        for (int i = 0; i < _wheelAxles.Length; i++)
        {
            sum += _wheelAxles[i].GetAverageRPM();
        }
        return sum / _wheelAxles.Length; 
    }
    public float GetWheelSpeed()
    {
        return GetAverageRPM() * _wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }

    private void UpdateDownForce()
    {
        float dF = Mathf.Clamp(_downForceFactor * linearVelocity, _downForceMin, _downForceMax);
        _rigidbody.AddForce(-transform.up * dF);
    }

    ///чтобы была зависимость от скорости - чем быстрее едешь, тем хуже поворачиваешь
    private void UpdateAngularDrag()
    {

        _rigidbody.angularDrag = Mathf.Clamp(_angularDragFactor * linearVelocity, _angularDragMin, _angularDragMax);
    }

    private void UpdateWheelAxles()
    {
        int amountMotorWheel = 0;

        for (int i = 0; i < _wheelAxles.Length; i++)
        {
            if (_wheelAxles[i].IsMotor)
            {
                amountMotorWheel += 2;
            }
        }


       for (int i = 0; i < _wheelAxles.Length; i++)
        {
            _wheelAxles[i].Update();

            //прикладываем какой-то момент, поворот

            _wheelAxles[i].ApplyMotorTorque(motorTorque / amountMotorWheel);
            _wheelAxles[i].ApplySteeringAngle(steerAngle, _wheelBaseLength);
            _wheelAxles[i].ApplyBreakTorque(brakeTorque);

        }
    }
}
