using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health = 10000;
    public bool invincible;

    public float invincibleCooldownTime = 2;
    private UIManager UI;
    
    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(int damage)
    {
        invincible = true;
        health -= damage;
        UI.UpdateHealth(health);
        print($"{health}: health ");
        print($"{damage}: damage ");
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
