using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public PlayerData playerData;
    public float criticDamageMultiplier=1.2f;
    public bool isDead;
    private AnimationController playerAnim;
    private CharMoveController charMove;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<AnimationController>();
        charMove = GetComponent<CharMoveController>();
        if (playerData.currentHealth <= 0) isDead = true;
        else isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float value, bool critic)
    {
        if (!isDead)
        {
            if (critic) value *= criticDamageMultiplier;
            playerData.currentHealth += value;
            if (value < 0) playerAnim.HitAnim();
            if (playerData.currentHealth > playerData.maxHealth)
            {
                playerData.currentHealth = playerData.maxHealth;
            }
            else if (playerData.currentHealth <= 0)
            {
                playerData.currentHealth = 0;
                charMove.canMove = false;
                UpdateLives(-1);
            }
            Debug.Log("VIDA PLAYER:" + playerData.currentHealth);
        }
    }

    private void Die()
    {
        playerAnim.DieAnim();
    }
    private void UpdateLives(int value)
    {
        if (value < 0)
        {
            Die();
            isDead = true;
        }
        if (playerData.lives+value >= 0)
        {
            playerData.lives += value;
            Debug.Log("Te moriste, te quedan estas vidas:"+playerData.lives);
        }
        if(playerData.lives<=0)
        {
            playerData.currentHealth = 0;
            Debug.Log("GAME OVER");
        }
    }
}
