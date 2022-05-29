using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] ParticleSystem _ps;
    [SerializeField] Vector3 _offset;
    [SerializeField] float _jetSpeed;
    [SerializeField] bool _jetsOn;
    [SerializeField] float _juice;

    UIManager _UIManager;
    void Start()
    {
        _UIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _playerRB.transform.position + _offset;
        CheckInput();
        CheckJuice();
        
    }
    void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (!_jetsOn)
            {
                _jetsOn = true;
                _ps.Play();
                print(_jetsOn);
            }
            _UIManager.UpdateJetJuice(_juice);

        }
        else
        {
            if (_jetsOn)
            {
                _jetsOn = false;
                _ps.Stop();
            }
        }
    }
    void CheckJuice()
    {
        if(_juice <= 0)
        {
            _jetsOn = false;
            _ps.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (!_jetsOn) return;

        _playerRB.AddForce(Vector3.up * _jetSpeed, ForceMode.Acceleration);
        _juice -= Time.deltaTime;

    }
}
