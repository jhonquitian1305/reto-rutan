using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;

    [SerializeField] private AudioSource impactAttackEnemySoundEffect;
    [SerializeField] private AudioSource deathEnemySoundEffect;
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

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
            Debug.Log("Enemigo muerto");
            deathEnemySoundEffect.Play();
        }
        Debug.Log("Vida del enemigo:" + currentHealth);
        impactAttackEnemySoundEffect.Play();
    }
}