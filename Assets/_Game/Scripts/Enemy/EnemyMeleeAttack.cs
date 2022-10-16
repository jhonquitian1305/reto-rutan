using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackDamage = 10f;
    public float cooldown = 1f;

    private float cooldownLeft;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetCooldownLeft();
    }
     
    private void SetCooldownLeft()
    {
        cooldownLeft -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (cooldownLeft <= 0)
            {
                collision.gameObject.GetComponent<PlayerHealthSystem>().UpdateHealth(-attackDamage);
                cooldownLeft = cooldown;
            }
        }
    }
}
