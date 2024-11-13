using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelPlayerModuleData", menuName = "ScriptableObjects/LevelPlayerModuleScriptableObject", order = 1)]
public class LevelPlayerModule : ScriptableObject
{
    [SerializeField] private int _levelModule;
    [SerializeField] private int _currencyNeeded;
    [SerializeField] private int _valueToAddToPile;
    [SerializeField] private Color _playerColor;

    public int LevelModule => _levelModule;
    public int CurrencyNeeded => _currencyNeeded;
    public int ValueToAddToPile => _valueToAddToPile;
    public Color PlayerColor => _playerColor;
}
