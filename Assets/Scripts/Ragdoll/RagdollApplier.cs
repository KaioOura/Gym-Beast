using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollApplier : MonoBehaviour
{
    [SerializeField] private AttackPhysics attackPhysics;
    
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hit")) return;

        if (other.TryGetComponent(out RagdollWrapper ragdollWrapper))
        {
            ragdollWrapper.UpdateRagdollState(true, attackPhysics);
        }
    }
}
