using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float MyPlayerSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private ConfigurableJoint _hipJoint;
    [SerializeField] private Rigidbody _hip;
    [SerializeField] private Transform _animTransform;
    [SerializeField] private GameObject _jetPack;
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
    public TextMeshProUGUI UIText;
    private bool canJump = false;
    public GameObject backgroundMusic;
    public GameObject skydiveSoundRegion;
    public GameObject skydiveSoundRegion2;
    public Rigidbody[] playerRBs;
    public GameObject dynamite;
    private UIManager _uiManager;
    [SerializeField] GeneralEventSO _dead;
    public bool dead;
    public bool autoDrop;

    private void OnEnable()
    {
        _dead.OnRaiseEvent += DeadChanges;
    }
    
    private void OnDisable()
    {
        _dead.OnRaiseEvent -= DeadChanges;
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

    
        playerRBs = GetComponentsInChildren<Rigidbody>();
        _uiManager = FindObjectOfType<UIManager>();
        playerHealth = GetComponent<Health>();
        _rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        _jetPack.SetActive(false);
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
        canJump = true;
        GoUI.SetActive(true);
        UIText.text = "GO!";
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
            HandleMovement();
            CheckFalling();
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space) || autoDrop)
        {
            if (_inAirPlane)
            {
                _inAirPlane = false;
                backgroundMusic.SetActive(true);
                skydiveSoundRegion.SetActive(true);
                skydiveSoundRegion2.SetActive(true);
                _targetAnimator.SetBool(_run, false);
                _targetAnimator.SetBool(_fall, true);
                _falling = true;
                autoDrop = false;
                canJump = false;
                
                ChangeCameraAnim(_playerCam);
                foreach (Rigidbody rb in _rbs)
                {
                    rb.isKinematic = false;
                }
            }
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

            if (!_falling)
            {
                _running = true;
                _targetAnimator.SetBool(_run, true);
            }

            
        }
        else
        {
            _running = false;
            _targetAnimator.SetBool(_run, false);
        }

        if(_direction != Vector3.zero)
        {
            HandleRotation(_direction);
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
    void CheckFalling()
    {
        if (IsGrounded())
        {
            if (dead)
            {
                _uiManager.ShowLoseScreen();
                MyPlayerSpeed = 0;
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
            }
            
            if (_falling)
                _falling = false;

            _targetAnimator.SetBool(_fall, false);
;       }
        

    }
    void DeadChanges()
    {
        _targetAnimator.SetBool(_fall, false);
        _jetPack.SetActive(true);
    }
    bool IsGrounded()
    {
         return Physics.Raycast(_hip.gameObject.transform.position, Vector3.down, out RaycastHit hitinfo, 2f, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_hip.gameObject.transform.position, Vector3.down * 2f);
    }

}
