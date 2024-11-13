using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNotifier : MonoBehaviour
{

    [SerializeField] private string _tagToCheck;
    public event Action<Collider> OnCollision;


    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_tagToCheck))
            return;

        OnCollision?.Invoke(other);
    }
}
