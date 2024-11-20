using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Movement _movement;
    [SerializeField] private AttackController _attackController;
    [SerializeField] private EmpilhamentoController _empilhamentoController;
    [SerializeField] private AnimatorRef _animatorRef;


    // Start is called before the first frame update
    void Start()
    {
        InitMovement();
        InitEmpilhamento();
        InitAttack();
    }

    void OnDestroy()
    {
        _inputManager.Joystick.OnMovement -= _movement.GetInput;
        _inputManager.AttackButton.onClick.RemoveListener(() => _attackController.OnAttack());
    }

    void InitMovement()
    {
        _movement.Init(_rb, _animatorRef.Animator);

        _inputManager.Joystick.OnMovement += _movement.GetInput;
    }

    void InitAttack()
    {
        _attackController.Init(_animatorRef.Animator);

        _inputManager.AttackButton.onClick.AddListener(() => _attackController.OnAttack());
    }

    void InitEmpilhamento()
    {
        _empilhamentoController.Init(_rb);
    }
}
