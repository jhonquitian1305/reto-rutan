using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image healthBarForeground;
    private EnemyHealthSystem enemyHealth;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);
    }

    public void UpdateHealthBarPercentage(float healthPercentage)
    {
        healthBarForeground.fillAmount = healthPercentage;
    }
}
