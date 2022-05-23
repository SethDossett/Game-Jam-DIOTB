using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingPowerUp : MonoBehaviour
{
    // Ring Color: 0 = Red, 1 = Green, 2 = Blue;
    [SerializeField] int RingColor;


    Transform _ring;
    Vector3 _position;
    Vector3 _scale;

    private void Start()
    {
        _ring = transform.parent;

        _position = new Vector3(Random.Range(-300f, 300f), Random.Range(30f, 180f), Random.Range(-300, 300));
        _scale = new Vector3(Random.Range(1f, 3f), 1f, 1f);
        _scale.y = _scale.x;
        _scale.z = _scale.x;
        _ring.transform.position = _position;
        _ring.rotation = Random.rotation;
        _ring.localScale = _scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(RingColor == 0) RedPowerUp();

            if (RingColor == 1) GreenPowerUp();

            if (RingColor == 2) BluePowerUp();
        }
    }


    void RedPowerUp()
    {
        print("Hit Red Power Up");
    }
    void GreenPowerUp()
    {
        print("Hit Green Power Up");
    }
    void BluePowerUp()
    {
        print("Hit Blue Power Up");
    }
}
