using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private float _volumeModifier;
    [SerializeField] private float _pitchModifier;
    [SerializeField] private float _rpmModifier;
    [SerializeField] private float _basePitch = 1.0f;
    [SerializeField] private float _baseVolume = 0.4f;
    private AudioSource _engineAudioSource;
    private void Start()
    {
        _engineAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _engineAudioSource.pitch = _basePitch + _pitchModifier * ((_car.engineRPM / _car.engineMaxRPM) * _rpmModifier);
        _engineAudioSource.volume = _baseVolume + _volumeModifier * (_car.engineRPM / _car.engineMaxRPM);
    }
}
