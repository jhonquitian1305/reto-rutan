using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;
    // Start is called before the first frame update
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
        maxHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } else if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
