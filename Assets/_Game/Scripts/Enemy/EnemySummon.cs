using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummon : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float cooldown = 10f;
    private CasterAnimController casterAnimController;
    public float cooldownLeft;

    // Start is called before the first frame update
    void Start()
    {
        casterAnimController = GetComponent<CasterAnimController>();
        cooldownLeft = 1;
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

    public void SummonEnemy()
    {
        if (cooldownLeft <= 0)
        {
            StartCoroutine(summonEnemy(1.5f));
            cooldownLeft = cooldown;
            casterAnimController.SummonAnim();
        }
    }
    IEnumerator summonEnemy(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        Instantiate(enemyPrefab, transform.position + transform.right, transform.rotation);
    }
}
