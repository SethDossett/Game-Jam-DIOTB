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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            PlayerController pc = other.gameObject.GetComponentInParent<PlayerController>();

            if (RingColor == 0) StartCoroutine(RedPowerUp(pc, rb));
                                
            if (RingColor == 1) StartCoroutine(GreenPowerUp(pc, rb));

            if (RingColor == 2) StartCoroutine(BluePowerUp(pc, rb));
        }
    }


    IEnumerator RedPowerUp(PlayerController pc , Rigidbody rb)
    {
        print("Hit Red Power Up");
        yield break;
        //rb.AddRelativeForce(Vector3.up * 500f, ForceMode.Impulse);
    }
    IEnumerator GreenPowerUp(PlayerController pc, Rigidbody rb)
    {
        pc.MyPlayerSpeed = 3000f;
        yield return new WaitForSeconds(1f);
        pc.MyPlayerSpeed = 500f;
        yield break;
    }
    IEnumerator BluePowerUp(PlayerController pc, Rigidbody rb)
    {
        GameObject player = pc.gameObject;

        Instantiate(player);
        yield break;
    }
}
