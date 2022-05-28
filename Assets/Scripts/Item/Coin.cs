using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int _coinAmount;
    string _colliderTag = "Player";
    Vector3 _coinAxis = Vector3.up;
    bool _coinCollected = false;
    MeshRenderer _meshRenderer;
    AudioSource _audioSource;
    ParticleSystem _ps;
    PointSystem _pointSystem;
    UIManager _UIManager;
    
    void Start()
    {
        _pointSystem = FindObjectOfType<PointSystem>();
        _UIManager = FindObjectOfType<UIManager>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _ps = GetComponent<ParticleSystem>();
        _meshRenderer.enabled = true;
    }
    
    void Update()
    {
        transform.Rotate(45f * Time.deltaTime * _coinAxis, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_coinCollected) return;

        if (other.gameObject.CompareTag(_colliderTag))
        {
            if (!_coinCollected)
            {
                CollectCoin();
            }
        }
    }

    void CollectCoin()
    {
        _coinCollected = true;
        _audioSource.Play();
        _ps.Play();
        _meshRenderer.enabled = false;
        _pointSystem.AddPoints(_coinAmount);
        _UIManager.UpdateCoins();
        Destroy(gameObject, 2f);
    }
}
