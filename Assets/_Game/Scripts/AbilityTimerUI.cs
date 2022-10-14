using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTimerUI : MonoBehaviour
{
    [Header("Fireball")]
    public Image fireballImage;
    public SpellController fireballController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetFireballImage();
    }

    private void SetFireballImage()
    {
        fireballImage.fillAmount = fireballController.CooldownPercentage();
    }
}
