using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private ConfigurableJoint _hipJoint;
    [SerializeField] private Rigidbody _hip;
    [SerializeField] private Transform _animTransform;
    Rigidbody[] _rbs;

    [SerializeField] Transform _camTransform;
    [SerializeField] Transform _airplane;
    Vector3 _offset = new Vector3(0, 2.5f, 0);

    [SerializeField] Animator _camAnim;
    [SerializeField] private Animator _targetAnimator;
    int _run = Animator.StringToHash("Run");
    int _fallingCam = Animator.StringToHash("FreeFallCam");
    int _playerCam = Animator.StringToHash("PlayerCam");

    private bool _running = false;
    private bool _inAirPlane = true;
    void Start()
    {
        _rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _rbs)
        {
            rb.isKinematic = true;
        }

        transform.position = _airplane.position + _offset;
    }
    void Update()
    {
        if (_inAirPlane)
        {
            transform.position = _airplane.position + _offset;
        }
        else
        {
            _camAnim.Play(_fallingCam);
            foreach (Rigidbody rb in _rbs)
            {
                rb.isKinematic = false;
            }
            HandleMovement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_inAirPlane)
                _inAirPlane = false;

        }

    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        direction = Quaternion.AngleAxis(_camTransform.rotation.eulerAngles.y, Vector3.up) * direction;
        direction.Normalize();

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            _hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            _hip.AddForce(direction * _speed * Time.fixedDeltaTime, ForceMode.Impulse);

            _running = true;
        }
        else
        {
            _running = false;
        }

        if(direction != Vector3.zero)
        {
            HandleRotation(direction);
        }

        _targetAnimator.SetBool(_run, _running);
    }
    void HandleRotation(Vector3 dir)
    {
        //Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        //
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        //_animTransform.rotation = transform.rotation;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
