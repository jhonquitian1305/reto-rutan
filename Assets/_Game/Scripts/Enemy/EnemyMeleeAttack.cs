using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackDamage = 10f;
    public float cooldown = 3f;
    public MeleeAnimController meleeAnimController;

    private float cooldownLeft;

    // Start is called before the first frame update
    void Start()
    {
        meleeAnimController = GetComponent<MeleeAnimController>();
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

    public void MeleeAttack()
    {
        if (cooldownLeft <= 0)
        {
            meleeAnimController.AttackAnim();
            cooldownLeft = cooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&!other.isTrigger)
        {
            other.GetComponent<PlayerHealthSystem>().UpdateHealth(-10, false);
        }
    }
}
