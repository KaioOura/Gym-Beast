using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventPLACEHOLDER : MonoBehaviour
{
    //This script is place holder. We need a system that tracks the current animation's frame so we can decide when to on/off the hit box
    //We can extend it's use to trigger other features like sounds, VFX, camera play, physics, etc
    [SerializeField] private GameObject _attackHitBox;

    void OnAttackHitBoxChange(float isOn)
    {
        _attackHitBox.SetActive(isOn == 0 ? false : true);
    }
}
