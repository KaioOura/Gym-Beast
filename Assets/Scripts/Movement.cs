using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;


    private Rigidbody _rb;
    private Animator _animator;

    private Vector2 _input;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MakeMovement();
        UpdateDirection();
    }

    public void Init(Rigidbody rb, Animator animator)
    {
        _rb = rb;
        _animator = animator;
    }

    public void GetInput(Vector2 input)
    {
        _input = input;
    }

    void MakeMovement()
    {
        _rb.velocity = new Vector3(_input.x * _speed, _rb.velocity.y, _input.y * _speed);
        _animator.SetFloat("Speed", Mathf.Abs(_input.magnitude));
    }

    void UpdateDirection()
    {
        if (_input.magnitude == 0)
            return;

        Vector3 direction = new Vector3(_input.x, 0, _input.y);
        Quaternion targetRot = Quaternion.LookRotation(direction);

        _rb.transform.rotation = Quaternion.Slerp(_rb.transform.rotation, targetRot, _rotationSpeed * Time.deltaTime);
    }
}
