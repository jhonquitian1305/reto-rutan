using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator anim;
    EnemyController enemy;

    private void Awake()
    {
        enemy = FindObjectOfType<EnemyController>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RunAnim();
    }
    void RunAnim()
    {
        if (enemy.enemyMove)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }
}
