using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    private float cycleTime;

    public float fireRate = 2f;
    public GameObject spellBallPrefab;
    public Transform originPoint;


    void Start()
    {
        
    }

    // Update is called once per frame
    public float CooldownTime()
    {
       if(cycleTime - Time.time > 0)
        {
            return cycleTime - Time.time;
        }
        else
        {
            return 0;
        }
    }

    public float CooldownPercentage()
    {
        return 1-(CooldownTime() / fireRate);
    }

    public void Shoot()
    {
        Vector3 originPosition = originPoint.position + originPoint.forward*0.5f;
        if (Time.time > cycleTime)
        {
            cycleTime = Time.time + fireRate;
            GameObject spellBall = Instantiate(spellBallPrefab, originPosition, Quaternion.identity);
            spellBall.GetComponent<SpellBall>().OriginGameObject = gameObject;
        }
    }
}
