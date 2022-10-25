using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyHealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float criticDamageMultiplier=1.5f;
    public ElementType weaknessElementType;
    public CasterAnimController casterAnimController;


    private float currentHealth;
    private float currentHealthPercentage;
    private EnemyHealthBar healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        currentHealthPercentage = currentHealth / maxHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        casterAnimController = GetComponent<CasterAnimController>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Die()
    {
        casterAnimController.DieAnim();
        StartCoroutine(SelfDestruct());
    }
    public void UpdateHealth(float value, bool critic)
    {
        if (critic) value *= criticDamageMultiplier;
        currentHealth += value; 
        if (currentHealth > maxHealth)
        {
            casterAnimController.GetHitAnim();
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
            Debug.Log("Enemigo muerto");
        }
        else
        {
            casterAnimController.GetHitAnim();
        }
        Debug.Log("Vida del enemigo:" + currentHealth);
        currentHealthPercentage = currentHealth / maxHealth;
        healthBar.UpdateHealthBarPercentage(currentHealthPercentage);
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}