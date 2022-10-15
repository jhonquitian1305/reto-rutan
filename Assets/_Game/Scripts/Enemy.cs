using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
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
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attackCooldown <= 0)
            {

                collision.gameObject.GetComponent<HealthSystem>().UpdateHealth(-attackDamage);
                attackCooldown = attackSpeed;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
    }
}
