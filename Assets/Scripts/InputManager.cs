using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _attackButton;

    public Joystick Joystick => _joystick;
    public Button AttackButton => _attackButton;

}
