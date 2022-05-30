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
    private PlayerController playerController;
    [SerializeField] GeneralEventSO _dead;
    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<UIManager>();
        playerController = GetComponentInParent<PlayerController>();
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
            _dead.RaiseEvent();
            playerController.dead = true;
            playerController.MyPlayerSpeed = 20f;
            UI.ShowDeadScreen();
        }
    }

    IEnumerator SloMo()
    {
        Time.timeScale = .3f;
        while (Time.timeScale <1f)
        {
            Time.timeScale += Time.deltaTime * .4f;
            yield return null;
        }
    }
    void InvincibleCooldown()
    {
        invincible = false;
    }
}
