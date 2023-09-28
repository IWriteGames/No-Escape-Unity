using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;
    private int maxHealth = 200;
    public int currentHealth;

    public float invincibleLength = 1f;
    private float invincCounter;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHealthUI(currentHealth);
    }

    void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                currentHealth = 0;
                GameManager.Instance.Defeat();
            }
            invincCounter = invincibleLength;
            UIManager.Instance.UpdateHealthUI(currentHealth);
        }
    }

    public void HealPlayer()
    {
        if(currentHealth >= 100 )
        {
            currentHealth = maxHealth;
        } else {
            Debug.Log("Current" + currentHealth);
            currentHealth = currentHealth + 100;
        }
        UIManager.Instance.UpdateHealthUI(currentHealth);
    }
}
