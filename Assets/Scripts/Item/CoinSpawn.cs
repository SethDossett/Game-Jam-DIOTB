using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField] GeneralEventSO _dead;
    [SerializeField] GameObject _coin;
    GameObject[] _coins;

    [SerializeField] bool _random;
    private void OnEnable()
    {
        _dead.OnRaiseEvent += Spawn;
    }
    private void OnDisable()
    {
        _dead.OnRaiseEvent -= Spawn;
    }


    void Start()
    {
        
    }

    void Spawn()
    {
        if (_random) RandomCoinSpawn();
        else ActivateCoins();
    }

    void ActivateCoins()
    {
        foreach (var coin in _coins)
        {
            if (!coin.activeInHierarchy)
            {
                coin.SetActive(true);
            }
        }
    }

    void RandomCoinSpawn()
    {

        for(int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-245f, 245f), Random.Range(15f, 200f), Random.Range(-250, 180f));
            Instantiate(_coin, pos, _coin.transform.rotation);
        }
        
    }
}
