using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    public float meleeAttackDamage = 10f;
    public float meleeAttackSpeed = 1f;

    public float rangeAttackDamage = 10f;
    public float rangeAttackSpeed = 1f;

    private float meleeAttackCooldown;
    private float rangeAttackCooldown;
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
        meleeAttackCooldown -= Time.deltaTime;
        rangeAttackCooldown -= Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (meleeAttackCooldown <= 0)
            {
                collision.gameObject.GetComponent<HealthSystem>().UpdateHealth(-meleeAttackDamage);
                meleeAttackCooldown = meleeAttackSpeed;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
