using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float timeTillDisappear = 5f;
    float timer;



    void Start()
    {
        timer = 0;
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer < timeTillDisappear)
        {
            transform.Translate(_speed * Time.deltaTime * transform.forward);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    


}
