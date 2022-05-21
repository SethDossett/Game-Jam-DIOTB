using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private ConfigurableJoint _hipJoint;
    [SerializeField] private Rigidbody _hip;

    [SerializeField] private Animator _targetAnimator;
    int _run = Animator.StringToHash("Run");

    private bool _running = false;
    void Start()
    {

    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            _hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            _hip.AddForce(direction * _speed, ForceMode.Impulse);

            _running = true;
        }
        else
        {
            _running = false;
        }

        _targetAnimator.SetBool(_run, _running);
    }
}
