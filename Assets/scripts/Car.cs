using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    private CarChassis _chassis;

    [SerializeField] private float _maxSteerAngle;
    [SerializeField] private float _maxBrakeTorque;

    //[SerializeField] private float _maxMotorTorque;
    [Header("Engine")]
    [SerializeField] private AnimationCurve _engineTorqueCurve;
    [SerializeField] private float _engineMaxTorque;
    [SerializeField] private float _engineTorque;
    [SerializeField] private float _engineRPM;
    [SerializeField] private float _engineMinRPM;
    [SerializeField] private float _engineMaxRPM;

    [Header("Gearbox")]
    [SerializeField] private float[] _gears;
    [SerializeField] private float _finalDriveRatio; //финальная передача
    [SerializeField] private int _selectedGearIndex;
    [SerializeField] private float _upShifEngineRPM;
    [SerializeField] private float _downShifEngineRPM;
    public float rearGear;

    [SerializeField] private float _maxSpeed;
    public float LinearVelocity => _chassis.linearVelocity;
    public float WheelSpeed => _chassis.GetWheelSpeed();
    public float MaxSpeed => _maxSpeed;

    [Header("debug")]
    public float throttleControl;
    public float steerControl;
    public float brakeControl;
    public float linVelocity;
    public float selectedGear;


    void Start()
    {
        _chassis = GetComponent<CarChassis>();
    }

    private void Update()
    {
        linVelocity = LinearVelocity;

        UpdateEngineTorque();
        AutoGearShift();

        if (LinearVelocity >= _maxSpeed)
        {
            _engineTorque = 0;
        }
        _chassis.motorTorque = throttleControl * _engineTorque;
        _chassis.brakeTorque = brakeControl * _maxBrakeTorque;
        _chassis.steerAngle = steerControl * _maxSteerAngle;
    }

    private void UpdateEngineTorque()
    {
        _engineRPM = _engineMinRPM + Mathf.Abs(_chassis.GetAverageRPM() * selectedGear * _finalDriveRatio);
        _engineRPM = Mathf.Clamp(_engineRPM, _engineMinRPM, _engineMaxRPM);

        //вычисление крутящего момента
        _engineTorque = _engineTorqueCurve.Evaluate(_engineRPM / _engineMaxRPM) * _engineMaxTorque * _finalDriveRatio * Mathf.Abs(selectedGear);
    }

    /// <summary>
    /// метод позволяющий включить передачу
    /// </summary>
    public void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, _gears.Length -1);
        selectedGear = _gears[gearIndex];
        _selectedGearIndex = gearIndex;
    }
    public void UpGear()
    {
        ShiftGear(_selectedGearIndex + 1);
    }
    public void DownGear()
    {
        ShiftGear(_selectedGearIndex - 1);
    }
    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
    }
    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }
    public void ShiftToNeutralGear()
    {
        selectedGear = 0;
    }

    private void AutoGearShift()
    {
        if (selectedGear < 0)
        {
            return;
        }
        if (_engineRPM >= _upShifEngineRPM)
        {
            UpGear();
        }
        if (_engineRPM < _downShifEngineRPM)
        {
            DownGear();
        }            
    }
}
