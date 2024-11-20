using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Transform _visual;
    [SerializeField] private Transform bodyPos;
    [SerializeField] private RagdollWrapper _ragdollWrapper;
    [SerializeField] private CollisionNotifier _collisionNotifier;
    [SerializeField] private Transform _bodyBones;
    [SerializeField] private int _sellValue;


    private bool _canBePicked;
    private IEnumerator _pickRoutine;

    public Transform BodyBones => _bodyBones;
    public Transform BodyPos => bodyPos;
    public int SellValue => _sellValue;


    // Start is called before the first frame update
    void Start()
    {
        //_visual.rotation = Quaternion.Euler(90, 0, 0);
        _collisionNotifier.OnCollision += TryToAddToPile;
        _ragdollWrapper.OnRagdollUpdate += OnRagdollUpdateRecieved;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToAddToPile(Collider collider)
    {
        if (!_canBePicked)
            return;

        Debug.Log("Encostou no: " + collider);

        if (collider.TryGetComponent(out EmpilhamentoController empilhamentoController))
        {
            if (empilhamentoController.TryToAddToPile(this))
            {
                _ragdollWrapper.ResetRagdoll();
                _collisionNotifier.gameObject.SetActive(false);
            }
        }
    }

    public void OnRagdollUpdateRecieved(bool isOn)
    {
        _collisionNotifier.gameObject.SetActive(isOn);

        if (_pickRoutine != null)
        {
            StopCoroutine(_pickRoutine);
        }

        if (isOn)
        {
            _pickRoutine = EnablePickingUp();
            StartCoroutine(_pickRoutine);
        }
    }

    IEnumerator EnablePickingUp()
    {
        yield return new WaitForSeconds(1);
        _canBePicked = true;
    }

}
