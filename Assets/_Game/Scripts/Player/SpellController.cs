using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    public float spellDamage = 30f;
    public float spellMoveSpeed = 7f;
    public float spellRange = 7f;
    public float spellCooldown = 2f;

    public GameObject spellBallPrefab;
    public Transform originPoint;
    public Image spellIndicatorImage;

    public float cycleTime;


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
        return 1-(CooldownTime() / spellCooldown);
    }

    public void Shoot()
    {
        if (Time.time > cycleTime)
        {
            Vector3 originPosition = originPoint.position + originPoint.forward * 0.5f;
            GameObject spellBall = Instantiate(spellBallPrefab, originPosition, transform.rotation);
            spellBall.GetComponent<SpellBall>().originGameObject = gameObject;
            spellBall.GetComponent<SpellBall>().spellDamage = spellDamage;
            spellBall.GetComponent<SpellBall>().spellMoveSpeed = spellMoveSpeed;
            spellBall.GetComponent<SpellBall>().spellRange = spellRange;
            cycleTime = Time.time + spellCooldown;
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
