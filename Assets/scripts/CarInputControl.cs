using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car _car;

    const string verticalAxis = "Vertical";
    const string horizontalAxis = "Horizontal";
    void Update()
    {
        _car.throttleControl = Input.GetAxis(verticalAxis);
        _car.steerControl = Input.GetAxis(horizontalAxis);
        _car.brakeControl = Input.GetAxis("Jump");
    }
}
