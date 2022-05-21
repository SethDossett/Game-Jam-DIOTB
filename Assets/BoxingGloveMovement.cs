using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloveMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float punchStrength =10;
    public float punchInterval =2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Punch",1,punchInterval);
    }

    void Punch()
    {
        print("punch");
        rb.AddForce(transform.up * punchStrength);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
