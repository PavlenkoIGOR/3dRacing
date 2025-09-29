using TMPro;
using UnityEngine;

public class CarGearBoxIndicator : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _car.gearChanged += OnGearChanged;
    }

    private void OnDestroy()
    {
        _car.gearChanged -= OnGearChanged;
    }

    private void OnGearChanged(string gearName)
    {
        _text.text = gearName;
    }
}
