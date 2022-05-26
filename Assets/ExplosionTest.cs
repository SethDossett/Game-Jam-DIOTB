using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTest : MonoBehaviour
{

    public Transform explosionTransform;

    private Vector3 moveVector = Vector3.zero;

    private Vector3 explosionVelocity = Vector3.zero;

    public float explosionForce = 10f;

    private CharacterController characterController;

    private float timeSinceExplosion = 0;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void AddExplosionForce()
    {
        Vector3 explosionDirection = transform.position - explosionTransform.position;
        explosionVelocity = (explosionDirection.normalized * explosionForce)/explosionDirection.magnitude;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            AddExplosionForce();
            timeSinceExplosion = 0;
        }

        timeSinceExplosion += Time.deltaTime;
        explosionVelocity = Vector3.Lerp(explosionVelocity, Vector3.zero, timeSinceExplosion / 2);
        
//        characterController.velocity = explosionVelocity;
        characterController.Move( explosionVelocity);
    }
}
