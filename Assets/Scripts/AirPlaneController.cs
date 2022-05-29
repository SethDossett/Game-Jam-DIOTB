using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneController : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float timeTillDisappear = 30f;
    [SerializeField] float timeTillForcedDrop = 26f;
    float timer;

    PlayerController playerController;

    void Start()
    {
        timer = 0;
        playerController = FindObjectOfType<PlayerController>();
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer < timeTillDisappear)
        {
            transform.Translate(_speed * Time.deltaTime * transform.forward);

            if(timer >= timeTillForcedDrop)
            {
                if(playerController.autoDrop == false)
                {
                    playerController.autoDrop = true;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

    


}
