using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCheck : MonoBehaviour
{
    private Health playerHealth;

    public float damageMultiplier = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!playerHealth.invincible)
        {
            int hitStrength = (int) (other.impulse.magnitude * damageMultiplier*.1f);
            print("Damage " + other.impulse.magnitude * damageMultiplier*.1f);
            
            //pointSystem.AddPoints(hitStrength * hitStrength);
            playerHealth.TakeDamage(hitStrength);
        }
        
    }
}
