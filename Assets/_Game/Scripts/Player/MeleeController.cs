using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public Transform atkPoint;
    public float meleeDamage;
    private AnimationController playerAnim;
    private float cycleTime = 0;
    public float attackCooldown = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CooldownTime()
    {
        if (cycleTime - Time.time > 0)
        {
            return cycleTime - Time.time;
        }
        else
        {
            return 0;
        }
    }

    public float CooldownPercentage()
    {
        return 1 - (CooldownTime() / attackCooldown);
    }

    public void Attack()
    {
        if (CooldownTime() <= 0)
        {
            playerAnim.MeleeAttack();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealthSystem enemyHealth = other.gameObject.GetComponent<EnemyHealthSystem>();
            if (enemyHealth != null)
            {
                enemyHealth.UpdateHealth(-meleeDamage, false);
            }
        }

    }
}
