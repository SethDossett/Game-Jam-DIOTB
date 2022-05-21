using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TempSpawn : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",2,1);
    }

    void Spawn()
    {
        Instantiate(player, transform.position, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
