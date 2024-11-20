using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EmpilhamentoController : MonoBehaviour
{
    [SerializeField] private Transform _firstVector;
    [SerializeField] private float _deformVelocity = 5;
    [SerializeField] private float _returnVelocity = 2;
    [SerializeField] private float _bodyFollowSpeed = 10;
    [SerializeField] List<Body> _bodies;

    private int _maxBodies;

    public List<Body> Bodies => _bodies;

    Rigidbody _rb;

    void Start()
    {
        PlayerManager.Instance.OnMaxPileCountUpdate += UpdateMaxBodyPileCount;
        UpdateMaxBodyPileCount(PlayerManager.Instance.MaxBodyPileCount);
    }

    void OnDestroy()
    {
        PlayerManager.Instance.OnMaxPileCountUpdate -= UpdateMaxBodyPileCount;
    }

    void Update()
    {
        UpdateVector();

    }

    void FixedUpdate()
    {
        HandleBodies();
    }

    public void Init(Rigidbody rb)
    {
        _rb = rb;
    }

    void UpdateMaxBodyPileCount(int value)
    {
        _maxBodies = value;
    }

    public bool TryToAddToPile(Body body)
    {
        Debug.Log("Tentando adicionar");

        if (_bodies.Count >= _maxBodies || _bodies.Contains(body))
            return false;

        _bodies.Add(body);

        return true;
    }

    public void OnBodyBought(Body body)
    {
        _bodies.Remove(body);
    }

    void UpdateVector()
    {
        Vector3 myVelocity = -_rb.velocity;
        Quaternion toRotation = Quaternion.FromToRotation(_firstVector.up, -_rb.velocity);

        if (myVelocity != Vector3.zero)
        {
            _firstVector.rotation = Quaternion.Lerp(_firstVector.rotation, toRotation, _deformVelocity * Time.deltaTime);
        }
        else
        {
            Quaternion toStopRotation = Quaternion.FromToRotation(_firstVector.up, Vector3.zero);
            _firstVector.rotation = Quaternion.Lerp(_firstVector.rotation, toStopRotation, _returnVelocity * Time.deltaTime);
        }

    }

    void HandleBodies()
    {
        if (_bodies.Count <= 0 || Mathf.Abs(_firstVector.eulerAngles.z) <= 0.5f)
            return;

        for (int i = 0; i < _bodies.Count; i++)
        {
            if (i == 0)
            {
                Quaternion toRot = Quaternion.LookRotation(-_firstVector.up);
                _bodies[0].BodyBones.position = Vector3.Lerp(_bodies[0].BodyBones.position, _firstVector.position, _bodyFollowSpeed * Time.deltaTime);
                _bodies[0].BodyBones.rotation = Quaternion.Lerp(_bodies[0].BodyBones.rotation, toRot, _deformVelocity * Time.deltaTime);

                continue;
            }

            _bodies[i].BodyBones.position = Vector3.Lerp(_bodies[i].BodyBones.position, _bodies[i - 1].BodyPos.position, _bodyFollowSpeed * Time.deltaTime);
            _bodies[i].BodyBones.rotation = Quaternion.Lerp(_bodies[0].BodyBones.rotation, _bodies[i - 1].BodyPos.rotation, _deformVelocity * Time.deltaTime);

        }
    }
}
