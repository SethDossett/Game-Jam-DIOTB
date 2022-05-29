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

    private Vector3 collisionNormal;
    private void Start()
    {
        sounds = FindObjectOfType<CharacterSounds>();
        _ring = transform.parent;
        _position = new Vector3(Random.Range(-300f, 300f), Random.Range(25f, 382f), Random.Range(-300, 230));
        _scale = new Vector3(Random.Range(.7f, 3f), 1f, 1f);
        _scale.y = _scale.x;
        _scale.z = _scale.x;
        _ring.transform.position = _position;
        _ring.rotation = Random.rotation;
        _ring.localScale = _scale;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_ringCollected) return;

        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.gameObject.GetComponentInParent<PlayerController>();

            if (RingColor == 0) StartCoroutine(RedPowerUp(pc));
                                
            if (RingColor == 1) StartCoroutine(GreenPowerUp(pc));

            if (RingColor == 2) StartCoroutine(BluePowerUp(pc));
            _ringCollected = true;
            sounds.PlaySound("RingPowerUp");
            Invoke("ResetRingCollected",1f);
        }
    }

    void ResetRingCollected()
    {
        _ringCollected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_ringCollected) return;
        
         collisionNormal = transform.forward;

        if (collisionNormal.y < 0)
        {
            collisionNormal *= -1f;
        }
            
    }

    IEnumerator RedPowerUp(PlayerController pc)
    {
        print("adding push force");
        foreach (var rb in FindObjectOfType<PlayerController>().playerRBs)
        {
            rb.AddForce(collisionNormal * ringPushForce,ForceMode.VelocityChange );
        }
        
        yield break;
        //rb.AddRelativeForce(Vector3.up * 500f, ForceMode.Impulse);
    }
    
    IEnumerator GreenPowerUp(PlayerController pc)
    {

        float ogSpeed = pc.MyPlayerSpeed;
        pc.MyPlayerSpeed *= 3.4f;
        yield return new WaitForSeconds(4f);
        pc.MyPlayerSpeed = ogSpeed;
        yield break;
    }
    
    IEnumerator BluePowerUp(PlayerController pc)
    {
        print("Set active to true");
        FindObjectOfType<PlayerController>().dynamite.SetActive(true);

        yield break;
    }
}
