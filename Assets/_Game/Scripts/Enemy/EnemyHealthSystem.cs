using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyHealthSystem : MonoBehaviour
{
    public float maxHealth;
    public ElementType weaknessElementType;
    private float currentHealth;
    private float currentHealthPercentage;
    private EnemyHealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        currentHealthPercentage = currentHealth / maxHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Die()
    {
        //muere
    }
    public void UpdateHealth(float value)
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            Debug.Log("Enemigo muerto");
        }
        Debug.Log("Vida del enemigo:" + currentHealth);
        currentHealthPercentage = currentHealth / maxHealth;
        healthBar.UpdateHealthBarPercentage(currentHealthPercentage);
    }
}