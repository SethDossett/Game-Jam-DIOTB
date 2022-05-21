using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLimb : MonoBehaviour
{
    [SerializeField] Transform _targetLimb;
    ConfigurableJoint _joint;

    Quaternion _targetInitialRotation;
    void Start()
    {
        _joint = GetComponent<ConfigurableJoint>();
        _targetInitialRotation = _targetLimb.transform.localRotation;
    }

    void FixedUpdate()
    {
        _joint.targetRotation = CopyRotation();
    }

    Quaternion CopyRotation()
    {
        return Quaternion.Inverse(_targetLimb.localRotation) * _targetInitialRotation; 
    }
}
