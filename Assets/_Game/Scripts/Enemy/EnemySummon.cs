using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float cooldown = 10f;
    private IEnemyAnimController casterAnimController;
    public float cooldownLeft;

    // Start is called before the first frame update
    void Start()
    {
        casterAnimController = GetComponent<IEnemyAnimController>();
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

    public void CastSummonEnemy()
    {
        if (cooldownLeft <= 0)
        {
            casterAnimController.SummonAnim();
            cooldownLeft = cooldown;
        }
    }

    public void SummonEnemy()
    {
        Instantiate(enemyPrefab, transform.position + transform.right, transform.rotation);
    }
}
