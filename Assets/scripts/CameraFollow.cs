using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _targer;
    [SerializeField] private Rigidbody _rb;

    [Header("Offset")]
    [SerializeField] private float _viewHeight;
    [SerializeField] private float _height;
    [SerializeField] private float _distance;

    [Header("Damping")]
    [SerializeField] private float _rotationDamping;
    [SerializeField] private float _heightDamping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetRotation = _targer.eulerAngles;

        //position
        Vector3 positionOffset = Quaternion.Euler(0, targetRotation.y, 0) * Vector3.forward * _distance;
        transform.position = _targer.position - positionOffset;
        transform.position = new Vector3(transform.position.x, _targer.position.y + _height, transform.position.z);

        transform.LookAt(_targer.position + new Vector3(0, _viewHeight, 0));
    }
}
