using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TickingTimeBomb : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 _offset;
    public Rigidbody _playerRB;

    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Explode", 3);
    }

    void Explode()
    {
        foreach( var rb in FindObjectOfType<PlayerController>().playerRBs)
        {
            Vector3 spherePoint = Random.insideUnitSphere * Random.RandomRange(100, 200f);
            if (spherePoint.y < 0) spherePoint.y *= -1;
            rb.AddForce(spherePoint ,ForceMode.VelocityChange);
            Instantiate(explosion, transform.position, quaternion.identity);
            FindObjectOfType<PointSystem>().AddPoints(500);
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = _playerRB.transform.position + _offset;

    }
}
