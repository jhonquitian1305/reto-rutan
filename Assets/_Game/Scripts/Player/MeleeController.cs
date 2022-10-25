using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public float meleeDamage;
    private AnimationController playerAnim;
    private float cycleTime = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponentInParent<AnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attack()
    {
        playerAnim.MeleeAttack();
        Debug.Log(playerAnim.comboCount);
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
