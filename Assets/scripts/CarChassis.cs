using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] _wheelAxels;

    public float motorTorque;
    public float brakeTorque;
    public float steerAngle;

    private void FixedUpdate()
    {
        UpdateWheelAxles();
    }

    private void UpdateWheelAxles()
    {
       for (int i = 0; i < _wheelAxels.Length; i++)
        {
            _wheelAxels[i].Update();

            //прикладываем какой-то момент, поворот

            _wheelAxels[i].ApplyMotorTorque(motorTorque);
            _wheelAxels[i].ApplySteeringAngle(steerAngle);
            _wheelAxels[i].ApplyBreakTorque(brakeTorque);

        }
    }
}
