using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollWrapper : MonoBehaviour
{
    [SerializeField] private Transform _mainRagdollTransform;
    [SerializeField] private Transform _chestRagDollTransform;
    [SerializeField] private Transform _headRagDollTransform;
    [SerializeField] private Collider _collider;

    private List<Collider> _ragDollColliders = new List<Collider>();
    private List<Rigidbody> _ragDollRigidBodies = new List<Rigidbody>();
    private Animator _ragDollAnimator;
    private Dictionary<Transform, TransformInfo> initialTransforms = new Dictionary<Transform, TransformInfo>();

    public event Action<bool> OnRagdollUpdate;

    // Start is called before the first frame update
    void Start()
    {
        GetRagdoll();
        UpdateRagdollState(false, new AttackPhysics());
    }

    void GetRagdoll()
    {
        var colliders = _mainRagdollTransform.GetComponentsInChildren<Collider>();
        var rigidbodies = _mainRagdollTransform.GetComponentsInChildren<Rigidbody>();
        _ragDollAnimator = _mainRagdollTransform.parent.GetComponent<Animator>();

        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ragdoll"))
            {
                _ragDollColliders.Add(collider);
            }
        }

        foreach (var rb in rigidbodies)
        {
            if (rb.gameObject.layer == LayerMask.NameToLayer("Ragdoll"))
            {
                _ragDollRigidBodies.Add(rb);
            }
        }

        //Get initial positions and rot from collider to reset body when needed
        StoreInitialTransforms(_mainRagdollTransform);
    }

    private void StoreInitialTransforms(Transform root)
    {
        foreach (Transform child in root.GetComponentsInChildren<Transform>())
        {
            initialTransforms[child] = new TransformInfo
            {
                position = child.localPosition,
                rotation = child.localRotation,
                scale = child.localScale
            };
        }
    }

    [ContextMenu("ResetRagDoll")]
    public void ResetRagdoll()
    {

        UpdateRagdollStateOff();
        // Reseta cada osso para a posição, rotação e escala iniciais
        foreach (var kvp in initialTransforms)
        {
            kvp.Key.localPosition = kvp.Value.position;
            kvp.Key.localRotation = kvp.Value.rotation;
            kvp.Key.localScale = kvp.Value.scale;
        }
    }

    public void UpdateRagdollState(bool isOn, AttackPhysics attackPhysics)
    {
        foreach (var collider in _ragDollColliders)
        {
            collider.enabled = isOn;
        }

        foreach (var rb in _ragDollRigidBodies)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = !isOn;
        }

        _ragDollAnimator.enabled = !isOn;

        if (isOn)
            _collider.gameObject.SetActive(false);

        OnRagdollUpdate?.Invoke(isOn);

        if (attackPhysics != null && attackPhysics.ApplyForce)
            ApplyForce(attackPhysics);
    }

    void ApplyForce(AttackPhysics attackPhysics)
    {
        switch (attackPhysics.BodyPartToApply)
        {
            case BodyParts.Head:
                {
                    break;
                }
            case BodyParts.Chest:
                {
                    Vector3 forceDir = transform.position - attackPhysics.HitterTransform.position;

                    if (_chestRagDollTransform.TryGetComponent(out Rigidbody rb))
                    {
                        rb.AddForce(forceDir * attackPhysics.Force.x, ForceMode.Impulse);
                    }

                    break;
                }
        }
    }

    [ContextMenu("RagOn")]
    public void UpdateRagdollStateOn()
    {
        foreach (var collider in _ragDollColliders)
        {
            collider.enabled = true;
        }

        foreach (var rb in _ragDollRigidBodies)
        {
            rb.isKinematic = false;
        }
    }

    [ContextMenu("RagOff")]
    public void UpdateRagdollStateOff()
    {
        foreach (var collider in _ragDollColliders)
        {
            collider.enabled = false;
        }

        foreach (var rb in _ragDollRigidBodies)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}

public class TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

public enum BodyParts
{
    Head,
    Chest
}
