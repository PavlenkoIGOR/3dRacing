using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class RPMIndicatorColor
{
    public float maxRPM;
    public Color color;
}

public class CarRPMIndicator : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private Image _image;
    [SerializeField] private RPMIndicatorColor[] _colors;


    // Update is called once per frame
    void Update()
    {
        _image.fillAmount = _car.engineRPM / _car.engineMaxRPM;

        for (int i = 0; i < _colors.Length; i++)
        {
            if (_car.engineRPM <= _colors[i].maxRPM)
            {
                _image.color = _colors[i].color;
                break;
            }
        }
    }
}
