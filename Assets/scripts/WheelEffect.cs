using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] _wheels;
    [SerializeField] private float _forwardSlipLimit;
    [SerializeField] private float _sidewaySlipLimit;
    [SerializeField] private GameObject _skipPrefab;
    [SerializeField] private ParticleSystem[] _wheelsSparks;
    [SerializeField] private AudioSource _audio;

    private WheelHit _wheelHit;
    private Transform[] _skidTrail;
    // Start is called before the first frame update
    void Start()
    {
        _skidTrail = new Transform[_wheels.Length];
    }

    // Update is called once per frame
    void Update()
    {
        bool isSlip = false;

        for (int i = 0; i < _wheels.Length; i++)
        {
            _wheels[i].GetGroundHit(out _wheelHit);

            if (_wheels[i].isGrounded)
            {
                if (_wheelHit.forwardSlip > _forwardSlipLimit || _wheelHit.sidewaysSlip > _forwardSlipLimit)
                {
                    if (_skidTrail[i] == null)
                    {
                        _skidTrail[i] = Instantiate(_skipPrefab).transform;
                    }

                    if (_audio.isPlaying == false)
                    {
                        _audio.Play();
                    }

                    if (_skidTrail[i] != null)
                    {
                        _skidTrail[i].position = _wheels[i].transform.position - _wheelHit.normal * _wheels[i].radius;
                        _skidTrail[i].forward = -_wheelHit.normal;

                        _wheelsSparks[i].transform.position = _skidTrail[i].position;
                        _wheelsSparks[i].Emit(1);

                    }
                    isSlip = true;
                    continue;
                }
            }

            _skidTrail[i] = null;
            _wheelsSparks[i].Stop();
        }

        if (isSlip == false)
        {
            _audio.Stop();
        }
    }
}
