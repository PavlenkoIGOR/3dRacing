using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _car.linVelocity.ToString("F0");
    }
}
