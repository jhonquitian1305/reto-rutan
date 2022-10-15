using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour, IDamage
{
    public float attackDamage = 10f;
    public float attackSpeed = 1f;

    private float attackCooldown;
    public void TakeDamage(float damage)
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetCooldowns();
    }
     
    private void SetCooldowns()
    {
        attackCooldown -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackCooldown <= 0)
            {
                collision.gameObject.GetComponent<PlayerHealthSystem>().UpdateHealth(-attackDamage);
                attackCooldown = attackSpeed;
            }
        }
    }

}
