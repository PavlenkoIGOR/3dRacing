using System;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private AnimationCurve _brakeCurve; //для настройки торможения в зависимости от скорости
    [SerializeField] private AnimationCurve _steerCurve;
    
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _autoBrakeStrength = 0.5f;

    const string verticalAxis = "Vertical";
    const string horizontalAxis = "Horizontal";
    const string jumpAxis = "Jump";

    private float _wheelSpeed;
    private float verticalAxisValue;
    private float horizontalAxisValue;
    private float handBrakeAxisValue;
    void Update()
    {
        _wheelSpeed = _car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBrake();
        UpdateSteer();



        //_car.brakeControl = handBrakeAxisValue;

        UpdateAutoBrake();

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    _car.UpGear();
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    _car.DownGear();
        //}
    }

    private void UpdateSteer()
    {

        _car.steerControl = _steerCurve.Evaluate(_car.WheelSpeed / _car.MaxSpeed) * horizontalAxisValue;
    }

    private void UpdateBrake()
    {
    }

    private void UpdateThrottleAndBrake()
    {
        if (Mathf.Sign(verticalAxisValue) == Mathf.Sign(_wheelSpeed) ||
            Mathf.Abs(_wheelSpeed) < 0.5f)
        {
            _car.throttleControl = Mathf.Abs(verticalAxisValue);
            _car.brakeControl = 0;
        }
        else
        {
            _car.throttleControl = 0;
            _car.brakeControl = _brakeCurve.Evaluate(_wheelSpeed / _car.MaxSpeed);
        }

        //Gears
        if (verticalAxisValue < 0 && _wheelSpeed > -0.5f && _wheelSpeed <= 0.5f)
        {
            _car.ShiftToReverseGear();
        }
        if (verticalAxisValue > 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f)
        {
            _car.ShiftToFirstGear();
        }
    }

    private void UpdateAxis()
    {
        verticalAxisValue = Input.GetAxis(verticalAxis);
        horizontalAxisValue = Input.GetAxis(horizontalAxis);
        handBrakeAxisValue = Input.GetAxis(jumpAxis);
    }

    private void UpdateAutoBrake()
    {
        if (verticalAxisValue == 0)
        {
            _car.brakeControl = _brakeCurve.Evaluate(_wheelSpeed / _car.MaxSpeed) * _autoBrakeStrength;
        }
    }
}
