using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    private float cycleTime;

    public float fireRate = 0.5f;
    public GameObject spellBallPrefab;
    public Transform originPoint;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Time.time > cycleTime)
        {
            cycleTime = Time.time + fireRate;
            GameObject spellBall = Instantiate(spellBallPrefab, originPoint.position, Quaternion.identity);
            spellBall.GetComponent<SpellBall>().OriginGameObject = gameObject;
        }
    }
}
