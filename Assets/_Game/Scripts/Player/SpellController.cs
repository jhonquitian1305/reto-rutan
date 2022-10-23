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
    public List<GameObject> spellBallPrefabs;

    public Transform originPoint;
    public GameObject spellIndicator;

    private int activeSpellIndex;
    private float cycleTime = 0;

    private AnimationController playerAnim;

    public int ActiveSpellIndex { get => activeSpellIndex; set => activeSpellIndex = value; }

    void Start()
    {
        if (spellIndicator != null) {
            spellIndicator.SetActive(false);
        }
        playerAnim = GetComponent<AnimationController>();
        activeSpellIndex = 0;

    }

    public void SetNextSpellAsActive()
    {
        activeSpellIndex++;
        if(activeSpellIndex> spellBallPrefabs.Count-1)
        {
            activeSpellIndex = 0;
        }
        Debug.Log(activeSpellIndex);
    }
    public void SetLastSpellAsActive()
    {
        activeSpellIndex--;
        if (activeSpellIndex < 0)
        {
            activeSpellIndex = spellBallPrefabs.Count-1;
        }
        Debug.Log(activeSpellIndex);
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
        playerAnim.CastAnimation();
    }

    private void CreateFire()
    {
        if (CooldownTime() <= 0)
        {
            Vector3 originPosition = originPoint.position;
            GameObject spellBall = Instantiate(spellBallPrefabs[activeSpellIndex], originPosition, transform.rotation);
            spellBall.GetComponent<SpellBall>().OriginGameObject = gameObject;
            spellBall.GetComponent<SpellBall>().SpellDamage = spellDamage;
            spellBall.GetComponent<SpellBall>().SpellMoveSpeed = spellMoveSpeed;
            spellBall.GetComponent<SpellBall>().SpellRange = spellRange;
            cycleTime = Time.time + spellCooldown;
        }
    }

    public void EnableIndicator(bool enabled)
    {
        if (spellIndicator != null)
        {
            spellIndicator.SetActive(enabled);
        }
    }
}
