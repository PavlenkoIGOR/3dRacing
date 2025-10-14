using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RaceInfo : ScriptableObject
{
    [SerializeField] string _sceneName;
    [SerializeField] Sprite _sprite;
    [SerializeField] string _title;

    public string sceneName => _sceneName;
    public Sprite sprite => _sprite;
    public string title => _title;
}
