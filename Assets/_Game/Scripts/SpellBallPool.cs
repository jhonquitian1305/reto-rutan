using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBallPool : MonoBehaviour
{
    public static SpellBallPool instance;
    public GameObject spellBallPrefab;

    private List<GameObject> pooledSpellBalls = new List<GameObject>();
    private int amountToPool = 10;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for(int i=0; i < amountToPool; i++)
        {
            GameObject spellBall = Instantiate(spellBallPrefab);
            spellBall.transform.parent = gameObject.transform;
            spellBall.SetActive(false);
            pooledSpellBalls.Add(spellBall);
        }
    }   

    // Update is called once per frame
    public GameObject GetPooledSpellBall()
    {
        for(int i=0; i<pooledSpellBalls.Count; i++)
        {
            if (!pooledSpellBalls[i].activeInHierarchy)
            {
                return pooledSpellBalls[i];
            }
        }

        return null;
    }
}
