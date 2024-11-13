using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Transform _visual;
    [SerializeField] private Transform bodyPos;
    [SerializeField] private RagdollWrapper _ragdollWrapper;
    [SerializeField] private CollisionNotifier _collisionNotifier;
    [SerializeField] private Transform _bodyBones;

    public Transform BodyBones => _bodyBones;
    public Transform BodyPos => bodyPos;


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
    }

}
