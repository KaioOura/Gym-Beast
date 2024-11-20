using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRef : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Animator Animator => _animator;

}
