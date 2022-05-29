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
    PlayerController _playerController;
    // Start is called before the first frame update
    void OnEnable()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _playerController.GoUI.SetActive(true);
        _playerController.UIText.text = "You got DYNAMITE!";
        Invoke("DisableUI", 1);
        Invoke("Explode", 3);
    }

    void Explode()
    {
        _playerController.GoUI.SetActive(false);
        foreach ( var rb in _playerController.playerRBs)
        {
            Vector3 spherePoint = Random.insideUnitSphere * Random.RandomRange(100, 200f);
            if (spherePoint.y < 0) spherePoint.y *= -1;
            rb.AddForce(spherePoint ,ForceMode.VelocityChange);
            Instantiate(explosion, transform.position, quaternion.identity);
            FindObjectOfType<PointSystem>().AddPoints(500);
            gameObject.SetActive(false);
        }
    }
    void DisableUI()
    {
        _playerController.GoUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = _playerRB.transform.position + _offset;

    }
}
