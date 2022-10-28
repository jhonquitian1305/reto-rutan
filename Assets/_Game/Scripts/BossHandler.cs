using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    public List<GameObject> essentialEnemies;
    public GameObject boss;
    void Start()
    {
        if (essentialEnemies == null || essentialEnemies.Count <= 0) {
            boss.SetActive(true);
            return;
        }
        foreach (GameObject enemy in essentialEnemies)
        {
            enemy.GetComponent<EnemyHealthSystem>().IsEssential = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfActivateBoss();
    }

    private void CheckIfActivateBoss()
    {
        if (essentialEnemies.Count <= 0) boss.SetActive(true);
    }
}
