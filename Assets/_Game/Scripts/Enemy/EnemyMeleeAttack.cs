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

    public void MeleeAttack()
    {
        if (cooldownLeft <= 0)
        {
            Debug.Log("ataque");
            cooldownLeft = cooldown;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Animación ataque
            MeleeAttack();
        }
    }
}
