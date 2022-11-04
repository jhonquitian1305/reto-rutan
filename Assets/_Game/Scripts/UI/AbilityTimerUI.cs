using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTimerUI : MonoBehaviour
{
    public SpellController spellController;
    public Image activeSpellImage;

    [Header("Fire")]
    public Image fireSpellImage;
    [Header("Holy")]
    public Image holySpellImage;
    [Header("Thunder")]
    public Image thunderSpellImage;

    private SpellBall activeSpell;
    // Start is called before the first frame update
    void Start()
    {
        spellController = GameObject.FindWithTag("Player").GetComponent<SpellController>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSpellImage();
  
    }

    private void SetSpellImage()
    {
        activeSpell = spellController.spellBallPrefabs[spellController.ActiveSpellIndex].GetComponent<SpellBall>();
        if (activeSpell.spellElementType == ElementType.Fire)
        {
            activeSpellImage.sprite = fireSpellImage.sprite;
        }
        else if (activeSpell.spellElementType == ElementType.Holy)
        {
            activeSpellImage.sprite = holySpellImage.sprite;
        }
        else if (activeSpell.spellElementType == ElementType.Thunder)
        {
            activeSpellImage.sprite = thunderSpellImage.sprite;
        }
        SetSpellImageFillAmount();
    }

    private void SetSpellImageFillAmount()
    {
        activeSpellImage.fillAmount = spellController.CooldownPercentage();
    }
}



