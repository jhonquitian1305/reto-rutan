using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData.currentHealth = playerData.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float value)
    {
        playerData.currentHealth += value;
        if (playerData.currentHealth > playerData.maxHealth)
        {
            playerData.currentHealth = playerData.maxHealth;
        } else if (playerData.currentHealth < 0)
        {
            playerData.currentHealth = 0;
            UpdateLives(-1);
        }
    }

    private void UpdateLives(int value)
    {
        playerData.lives += value;
        if (playerData.lives <= 0)
        {
            //GAME OVER
            return;
        }
        playerData.currentHealth = playerData.maxHealth; //Reinicia la vida
    }
}
