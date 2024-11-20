using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AttackPhysics
{
    [SerializeField] private Transform _hitterTransform;
    [SerializeField] private bool _applyForce;
    [SerializeField] private Vector2 _force;
    [SerializeField] private  BodyParts _bodyPartToApply;

    //Local/Global forces
    //Torque forces

    public Transform HitterTransform => _hitterTransform;
    public bool ApplyForce => _applyForce;
    public Vector2 Force => _force;
    public BodyParts BodyPartToApply => _bodyPartToApply;

}
