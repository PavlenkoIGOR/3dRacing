using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider[] _wheelsColliders;
    [SerializeField] private Transform[] _wheelsMeshes;
    [SerializeField] private float _torqueMotor;
    [SerializeField] private float _steeringAngle;
    [SerializeField] private float _torqueBreak;

    const string verticalAxis = "Vertical";
    const string horizontalAxis = "Horizontal";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _wheelsColliders.Length; i++)
        {
            _wheelsColliders[i].motorTorque = Input.GetAxis("Vertical") * _torqueMotor;
            _wheelsColliders[i].brakeTorque = Input.GetAxis("Jump") * _torqueBreak;

            Vector3 pos;
            Quaternion rot;
            _wheelsColliders[i].GetWorldPose(out pos, out rot);
            _wheelsMeshes[i].position = pos;
            _wheelsMeshes[i].rotation = rot;
        }

        _wheelsColliders[0].steerAngle = Input.GetAxis(horizontalAxis) * _steeringAngle;
        _wheelsColliders[1].steerAngle = Input.GetAxis(horizontalAxis) * _steeringAngle;
    }
}
