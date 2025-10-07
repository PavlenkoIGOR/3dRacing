using UnityEngine;

public class CameraShaker : CarCameraComponent
{
    [SerializeField] float _shakeAmount;
    [SerializeField] [Range(0.0f, 1.0f)] float _normalizeSpeedShake;


    // Update is called once per frame
    void Update()
    {
        if (_car.NormalizeLinearVelocity >= _normalizeSpeedShake)
        {
            transform.localPosition += Random.insideUnitSphere * _shakeAmount * Time.deltaTime;
        }
    }
}
