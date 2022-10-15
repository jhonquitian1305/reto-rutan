using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    private float cycleTime;

    public float fireRate = 2f;
    public GameObject spellBallPrefab;
    public Transform originPoint;
    public Image spellIndicatorImage;


    void Start()
    {
        if (spellIndicatorImage != null) {
            spellIndicatorImage.enabled = false;
        }
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
            GameObject spellBall = Instantiate(spellBallPrefab, originPosition, transform.rotation);
            spellBall.GetComponent<SpellBall>().OriginGameObject = gameObject;
        }
    }

    public void EnableIndicator(bool enabled)
    {
        if (spellIndicatorImage != null)
        {
            spellIndicatorImage.enabled = enabled;
        }
    }
}
