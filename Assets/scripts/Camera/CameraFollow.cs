using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rb;

    [Header("Offset")]
    [SerializeField] private float _viewHeight;
    [SerializeField] private float _height;
    [SerializeField] private float _distance;

    [Header("Damping")]
    [SerializeField] private float _rotationDamping;
    [SerializeField] private float _heightDamping;
    [SerializeField] private float _speedThreshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 velocity = _rb.velocity;
        Vector3 targetRotation = _target.eulerAngles;

        if (velocity.magnitude > _speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
        }

        //Lerp
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, _rotationDamping * Time.fixedDeltaTime);
        float currentHeight = Mathf.Lerp(transform.position.y, _target.position.y + _height, _heightDamping * Time.fixedDeltaTime);

        //position
        Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
        transform.position = _target.position - positionOffset;
        transform.position = new Vector3(transform.position.x,currentHeight, transform.position.z);

        transform.LookAt(_target.position + new Vector3(0, _viewHeight, 0));
    }
}
