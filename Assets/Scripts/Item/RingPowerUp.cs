using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RingPowerUp : MonoBehaviour
{
    // Ring Color: 0 = Red, 1 = Green, 2 = Blue;
    [SerializeField] int RingColor;


    Transform _ring;
    Vector3 _position;
    Vector3 _scale;
    public float ringPushForce = 10;
    private bool _ringCollected;
    private CharacterSounds sounds;
     
    private void Start()
    {
        sounds = FindObjectOfType<CharacterSounds>();
        _ring = transform.parent;
        _position = new Vector3(Random.Range(-300f, 300f), Random.Range(10f, 180f), Random.Range(-300, 230));
        _scale = new Vector3(Random.Range(1f, 3f), 1f, 1f);
        _scale.y = _scale.x;
        _scale.z = _scale.x;
        _ring.transform.position = _position;
        _ring.rotation = Random.rotation;
        _ring.localScale = _scale;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            PlayerController pc = other.gameObject.GetComponentInParent<PlayerController>();

            if (RingColor == 0) StartCoroutine(RedPowerUp(pc, rb));
                                
            if (RingColor == 1) StartCoroutine(GreenPowerUp(pc, rb));

            if (RingColor == 2) StartCoroutine(BluePowerUp(pc, rb));
            _ringCollected = true;
            sounds.PlaySound("RingPowerUp");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_ringCollected) return;

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        var collisionNormal = transform.position - other.ClosestPoint(transform.position);

        if (collisionNormal.y < 0)
        {
            collisionNormal *= -1f;
        }
            
        rb.AddForce(collisionNormal * ringPushForce);
    }

    IEnumerator RedPowerUp(PlayerController pc , Rigidbody rb)
    {
        print("Hit Red Power Up");
        yield break;
        //rb.AddRelativeForce(Vector3.up * 500f, ForceMode.Impulse);
    }
    IEnumerator GreenPowerUp(PlayerController pc, Rigidbody rb)
    {
        pc.MyPlayerSpeed = 1.5f;
        yield return new WaitForSeconds(1f);
        pc.MyPlayerSpeed = 1f;
        yield break;
    }
    IEnumerator BluePowerUp(PlayerController pc, Rigidbody rb)
    {
        GameObject player = pc.gameObject;

        Instantiate(player);
        yield break;
    }
}
