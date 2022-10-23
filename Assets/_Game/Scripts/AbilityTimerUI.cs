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

    }

    // Update is called once per frame
    void Update()
    {
        SetSpellImage();
  
    }

    private void SetSpellImage()
    {
        activeSpell = spellController.spellBallPrefabs[spellController.ActiveSpellIndex].GetComponent<SpellBall>();
        if (activeSpell.spellType.Equals("Fire", StringComparison.OrdinalIgnoreCase))
        {
            activeSpellImage.sprite = fireSpellImage.sprite;
        }
        else if (activeSpell.spellType.Equals("Holy", StringComparison.OrdinalIgnoreCase))
        {
            activeSpellImage.sprite = holySpellImage.sprite;
        }
        else if (activeSpell.spellType.Equals("Thunder", StringComparison.OrdinalIgnoreCase))
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



