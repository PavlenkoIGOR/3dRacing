using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction Finished;

    [SerializeField] float _time;

    float _value;

    public float Value { get => _value; set => _value = value; }

    // Start is called before the first frame update
    void Start()
    {
        _value = _time;
    }

    // Update is called once per frame
    void Update()
    {
        _value -= Time.deltaTime;

        if (_value <= 0)
        {
            enabled = false;

            Finished?.Invoke();
        }
    }
}
