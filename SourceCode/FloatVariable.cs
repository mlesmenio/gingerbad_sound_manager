using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FloatVariable" , menuName = "Gingerbad/FloatVariable" , order = 1)]
public class FloatVariable : ScriptableObject
{
    internal event Action onValueChanged;

    [Tooltip("Changing the value here does not trigger the onValueChanged event. To do so use the button right below.")] 
    [SerializeField] float _current_value;

    public float currentValue
    {
        get
        {
            return _current_value;
        }

        set
        {
            _current_value = value;

            onValueChanged?.Invoke();    
        }
    }
}
