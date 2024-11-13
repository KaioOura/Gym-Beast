using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;

    private float _timeAttack;
    private Animator _animator;

    public void Init(Animator animator)
    {
        _animator = animator;
    }

    public void OnAttack()
    {
        if (!CanAttack())
            return;

        _animator.SetTrigger("Attack");
        _timeAttack = Time.time + _attackCooldown;
    }

    bool CanAttack()
    {
        return Time.time >= _timeAttack;
    }
}
