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
    [SerializeField] LayerMask _groundLayer;

    [SerializeField] Transform _camTransform;
    [SerializeField] Transform _airplane;
    Vector3 _offset = new Vector3(0, 2.5f, 0);

    [SerializeField] Animator _camAnim;
    [SerializeField] private Animator _targetAnimator;
    int _run = Animator.StringToHash("Run");
    int _fall = Animator.StringToHash("Fall");
    int _fallingCam = Animator.StringToHash("FreeFallCam");
    int _playerCam = Animator.StringToHash("PlayerCam");

    private bool _falling = false;
    private bool _running = false;
    private bool _inAirPlane = true;
    void Start()
    {
        _rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _rbs)
        {
            rb.isKinematic = true;
        }

        if(_airplane != null)
        {
            transform.position = _airplane.position + _offset;
        }
    }
    void Update()
    {
        if (_inAirPlane)
        {
            if (_airplane != null)
            {
                transform.position = _airplane.position + _offset;
            }
        }
        else
        {
            ChangeCameraAnim(_playerCam);
            foreach (Rigidbody rb in _rbs)
            {
                rb.isKinematic = false;
            }
            HandleMovement();
            CheckFalling();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_inAirPlane)
                _inAirPlane = false;

            _falling = true;

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

            if(!_falling)
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

        if (!_falling)
        {
            _targetAnimator.SetBool(_fall, false);
            _targetAnimator.SetBool(_run, _running);
        }
    }
    void HandleRotation(Vector3 dir)
    {
        //Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        //
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        //_animTransform.rotation = transform.rotation;
    }

    void ChangeCameraAnim(int hash)
    {
        _camAnim.Play(hash);
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
    void CheckFalling()
    {
        if (IsGrounded())
        {
            if (_falling)
                _falling = false;
            else return;
;       }

        if (_falling)
        {
            _targetAnimator.SetBool(_run, false);
            _targetAnimator.SetBool(_fall, true);
        }

    }

    bool IsGrounded()
    {
        if (_falling)
        {
            return Physics.Raycast(_hip.gameObject.transform.position, Vector3.down, out RaycastHit hitinfo, 10f, _groundLayer);

        }
        else return false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_hip.gameObject.transform.position, Vector3.down * 10f);
    }
}
