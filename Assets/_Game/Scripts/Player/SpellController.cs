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

    public GameObject orbOrigin;
    public GameObject spellIndicator;

    private int activeSpellIndex=0;
    private float cycleTime = 0;

    private AnimationController playerAnim;

    public int ActiveSpellIndex { get => activeSpellIndex; set => activeSpellIndex = value; }

    void Start()
    {
        if (spellIndicator != null) {
            spellIndicator.SetActive(false);
        }
        playerAnim = GetComponent<AnimationController>();
        ChangeSpellIndicatorColor();
    }
    private void ChangeSpellIndicatorColor()
    {
        SpellBall activeSpell = spellBallPrefabs[activeSpellIndex].GetComponent<SpellBall>();
        Color newColor = Color.black;
        if (activeSpell.spellElementType == ElementType.Fire) newColor = new Color(1,0.33f,0,1);
        else if (activeSpell.spellElementType == ElementType.Holy) newColor = Color.yellow;
        else if (activeSpell.spellElementType == ElementType.Thunder) newColor = new Color(0,0.5f,1,1);
        spellIndicator.GetComponent<Projector>().material.SetColor("_MainColor", newColor);
        orbOrigin.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
    }

    public void SetNextSpellAsActive()
    {
        activeSpellIndex++;
        if(activeSpellIndex> spellBallPrefabs.Count-1)
        {
            activeSpellIndex = 0;
        }
        ChangeSpellIndicatorColor();
    }
    public void SetLastSpellAsActive()
    {
        activeSpellIndex--;
        if (activeSpellIndex < 0)
        {
            activeSpellIndex = spellBallPrefabs.Count-1;
        }
        ChangeSpellIndicatorColor();
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
            Vector3 originPosition = orbOrigin.transform.position;
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
