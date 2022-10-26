using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIController : MonoBehaviour
{
    public TextMeshProUGUI lives, score;
    public Image healthBar;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount=(playerData.currentHealth / playerData.maxHealth);
        lives.text = "X" + playerData.lives;
        score.text = "X" + playerData.score;
    }
}
