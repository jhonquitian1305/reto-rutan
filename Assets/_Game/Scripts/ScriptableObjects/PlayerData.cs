using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data", fileName = "New Player Data")]
public class PlayerData : ScriptableObject
{
    public int lives = 3;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float score = 0;

    public void SetPlayerData(int lives, float maxHealth, float currentHealth, float score)
    {
        this.lives = lives;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.score = score;
    }
}
