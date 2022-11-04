using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public float meleeDamage;
    public float bonusMultiplier=1;
    public float meleeCooldown=1f;
    public bool startCooldown;
    private AnimationController playerAnim;
    private float cooldownLeft = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponentInParent<AnimationController>();
        bonusMultiplier = 1;
        cooldownLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SetCooldownLeft();
        
    }

    public float CooldownPercentage()
    {
        return 1-(cooldownLeft / meleeCooldown);
    }

    public void Attack()
    {
        if(cooldownLeft<=0) playerAnim.MeleeAttack();
    }
    public void RestartCooldown()
    {
        cooldownLeft = meleeCooldown;
    }
    private void SetCooldownLeft()
    {
        if (cooldownLeft <= 0) return;
        cooldownLeft -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !other.isTrigger)
        {
            EnemyHealthSystem enemyHealth = other.gameObject.GetComponent<EnemyHealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.UpdateHealth(-(meleeDamage*bonusMultiplier), false);
            }
        }

    }
}
