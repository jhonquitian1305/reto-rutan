using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
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
        } else if (playerData.currentHealth <= 0)
        {
            playerData.currentHealth = 0;
            UpdateLives(-1);
        }
        Debug.Log("VIDA PLAYER:" + playerData.currentHealth);
    }

    private void UpdateLives(int value)
    {
        if (playerData.lives+value > 0)
        {
            playerData.lives += value;
            playerData.currentHealth = playerData.maxHealth; //Reinicia la vida
            Debug.Log("Te moriste, te quedan estas vidas:"+playerData.lives);
        }


        if(playerData.lives<=0)
        {
            playerData.currentHealth = 0;
            Debug.Log("GAME OVER");
        }
    }
}
