using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    private int health = 100;
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
        if (health <= 0) return;
        invincible = true;
        health -= damage;
        UI.UpdateHealth(health);
        Invoke("InvincibleCooldown",invincibleCooldownTime);
        if (health <= 0)
        {
            StartCoroutine("SloMo");
            FindObjectOfType<PlayerController>().dead = true;
            FindObjectOfType<PlayerController>().MyPlayerSpeed = 5f;
            BombMovement[] bombs = FindObjectsOfType<BombMovement>();
            foreach (var bomb in bombs)
            {
                bomb.agent.speed = .1f;
            }
            UI.ShowDeadScreen();
        }
    }

    IEnumerator SloMo()
    {
        Time.timeScale = .1f;
        while (Time.timeScale <1f)
        {
            Time.timeScale += Time.deltaTime * .2f;
            yield return null;
        }
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
