using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BombMovement : MonoBehaviour
{
    private bool walking = true;

    private Vector3 currentWaypoint;
    
    public float waypointRadius;
    public NavMeshAgent agent;
    private Animator anim;
    private List<Rigidbody> playerRBs;

    public float bombForce;
    public float upwardsForce;
    public int bombPoints;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine("RandomWaypointLoop");

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        playerRBs = new List<Rigidbody>();

        foreach (var o in playerObjects)
        {
            Rigidbody rb = o.GetComponent<Rigidbody>();
            if (rb != null)
            {
                playerRBs.Add(rb);
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        //print("collision with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            anim.Play("attack01");
            agent.speed = 0;
        }
    }

    public void PhysicsExplode()
    {
        foreach (var rb in playerRBs)
        {
            rb.AddExplosionForce(bombForce,transform.position,5000,upwardsForce);
        }
        FindObjectOfType<Health>().TakeDamage(15);
        FindObjectOfType<PointSystem>().AddPoints(bombPoints);
    }
    
    IEnumerator RandomWaypointLoop()   
    {
        while (walking)
        {
            yield return new WaitForSeconds(Random.Range(2, 8));
            PickNewWayPoint();
            //StartCoroutine("RandomWaypointLoop");
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void PickNewWayPoint()
    {
        currentWaypoint = new Vector3(Random.insideUnitCircle.x * waypointRadius,
            transform.position.y, Random.insideUnitCircle.y * waypointRadius);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(currentWaypoint);
        if(Vector3.Distance(transform.position,currentWaypoint) <10) PickNewWayPoint();
    }
}
