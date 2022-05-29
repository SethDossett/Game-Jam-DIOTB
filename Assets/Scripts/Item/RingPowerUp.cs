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
    PlayerController _pc;
    private Vector3 collisionNormal;
    private void Start()
    {
        _pc = FindObjectOfType<PlayerController>();
        sounds = FindObjectOfType<CharacterSounds>();
        _ring = transform.parent;
        _position = new Vector3(Random.Range(-260f, 260f), Random.Range(25f, 382f), Random.Range(-280, 230));
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
        PlayerController pc = _pc;
        if (other.CompareTag("Player"))
        {
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
        pc.GoUI.SetActive(true);
        pc.UIText.text = "BOOST";
        foreach (var rb in pc.playerRBs)
        {
            rb.AddForce(collisionNormal * ringPushForce,ForceMode.VelocityChange );
        }
        yield return new WaitForSeconds(1f);
        pc.GoUI.SetActive(false);
        yield break;
        //rb.AddRelativeForce(Vector3.up * 500f, ForceMode.Impulse);
    }
    
    IEnumerator GreenPowerUp(PlayerController pc)
    {
        pc.GoUI.SetActive(true);
        pc.UIText.text = "SPEED INCREASE!";
        float ogSpeed = pc.MyPlayerSpeed;
        pc.MyPlayerSpeed *= 3.4f;
        yield return new WaitForSeconds(1f);
        pc.GoUI.SetActive(false);
        yield return new WaitForSeconds(3f);
        pc.MyPlayerSpeed = ogSpeed;
        yield break;
    }
    
    IEnumerator BluePowerUp(PlayerController pc)
    {
        print("Set active to true");
        pc.dynamite.SetActive(true);

        yield break;
    }
}
