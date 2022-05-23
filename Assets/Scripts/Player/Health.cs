using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 100;
    public bool invincible;

    public float invincibleCooldownTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        invincible = true;
        health -= damage;
        Invoke("InvincibleCooldown",invincibleCooldownTime);
    }

    void InvincibleCooldown()
    {
        invincible = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
