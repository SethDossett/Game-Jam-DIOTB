using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayerBackDown : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRB;
    bool _push;

    private void OnTriggerEnter(Collider other)
    {
        if (_push) return;

        if (other.gameObject.CompareTag("Player"))
        {
            _push = true;
            Invoke("StopPush", 1);
        }
    }

    private void FixedUpdate()
    {
        if (!_push) return;

        _playerRB.AddForce(Vector3.down * 100f, ForceMode.Impulse);
        
    }

    void StopPush()
    {
        _push = false;
    }
}
