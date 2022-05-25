using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MyPlayerSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private ConfigurableJoint _hipJoint;
    [SerializeField] private Rigidbody _hip;
    [SerializeField] private Transform _animTransform;
    Rigidbody[] _rbs;
    Health playerHealth;
    [SerializeField] LayerMask _groundLayer;

    [SerializeField] Transform _camTransform;
    [SerializeField] Transform _airplane;
    Vector3 _offset = new Vector3(0, 2.5f, 0);
    Vector3 _direction;

    [SerializeField] Animator _camAnim;
    [SerializeField] private Animator _targetAnimator;
    int _run = Animator.StringToHash("Run");
    int _fall = Animator.StringToHash("Fall");
    int _fallingCam = Animator.StringToHash("FreeFallCam");
    int _playerCam = Animator.StringToHash("PlayerCam");

    private bool _falling = false;
    private bool _running = false;
    private bool _inAirPlane = true;
    
    public GameObject GoUI;
    private bool canJump = false;
    public GameObject backgroundMusic;
    public GameObject skydiveSoundRegion;
    public GameObject skydiveSoundRegion2;
    void Start()
    {
        playerHealth = GetComponent<Health>();
        _rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _rbs)
        {
            rb.isKinematic = true;
        }

        if(_airplane != null)
        {
            transform.position = _airplane.position + _offset;
        }
    
        Invoke("EnableJumping", 7);
    }

    void EnableJumping()
    {
        print("jump");
        canJump = true;
        GoUI.SetActive(true);
        Invoke("TurnOffGoUI", 3);

    }

    void TurnOffGoUI()
    {
        GoUI.SetActive(false);

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

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            if (_inAirPlane)
            {
                _inAirPlane = false;
                backgroundMusic.SetActive(true);
                skydiveSoundRegion.SetActive(true);
                skydiveSoundRegion2.SetActive(true);
                
            }
            _falling = true;

        }

    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _direction = new Vector3(horizontal, 0f, vertical).normalized;
        _direction = Quaternion.AngleAxis(_camTransform.rotation.eulerAngles.y, Vector3.up) * _direction;
        _direction.Normalize();

        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.z, _direction.x) * Mathf.Rad2Deg;

            _hipJoint.targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            //_hip.AddForce(_direction * MyPlayerSpeed * Time.fixedDeltaTime, ForceMode.Impulse);

            if(!_falling)
                _running = true;
        }
        else
        {
            _running = false;
        }

        if(_direction != Vector3.zero)
        {
            HandleRotation(_direction);
        }

        if (!_falling)
        {
            _targetAnimator.SetBool(_fall, false);
            _targetAnimator.SetBool(_run, _running);
        }
    }

    private void FixedUpdate()
    {
        if (_direction.magnitude >= 0.1f)
        {
            _hip.AddForce(_direction * MyPlayerSpeed, ForceMode.Impulse);
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

    private void OnCollisionEnter(Collision other)
    {
        print("hit");
        
         if (!playerHealth.invincible)
         {
             int hitStrength = (int)other.impulse.magnitude;
             //pointSystem.AddPoints(hitStrength * hitStrength);
             playerHealth.TakeDamage(hitStrength);
         }
        
    }
}
