using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyHealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float criticDamageMultiplier=1.5f;
    public ElementType weaknessElementType;
    public IEnemyAnimController casterAnimController;
    public bool isDead;
    public float deathAnimTime=2f;
    public SceneHandler sceneHandler;
    public BossHandler bossHandler;


    public bool isEssential = false;
    private float currentHealth;
    private float currentHealthPercentage;
    private EnemyHealthBar healthBar;

    public bool IsEssential { get => isEssential; set => isEssential = value; }

    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
        currentHealthPercentage = currentHealth / maxHealth;
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        casterAnimController = GetComponent<IEnemyAnimController>();
        GameObject sceneHandlerObject = GameObject.FindWithTag("SceneHandler");
        if(sceneHandlerObject != null) sceneHandler = sceneHandlerObject.GetComponent<SceneHandler>();
        GameObject bossHandlerObject = GameObject.FindWithTag("BossHandler");
        if (bossHandlerObject != null) bossHandler = bossHandlerObject.GetComponent<BossHandler>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void Die()
    {
        if(isEssential&& sceneHandler!=null) sceneHandler.essentialEnemies.Remove(gameObject);
        if (isEssential && bossHandler != null) bossHandler.essentialEnemies.Remove(gameObject);
        casterAnimController.DieAnim();
        isDead = true;
        StartCoroutine(SelfDestruct());
    }
    public void UpdateHealth(float value, bool critic)
    {
        if (isDead) return;

        if (critic) value *= criticDamageMultiplier;
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            //casterAnimController.GetHitAnim();
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            casterAnimController.GetHitAnim();
        }
        currentHealthPercentage = currentHealth / maxHealth;
        healthBar.UpdateHealthBarPercentage(currentHealthPercentage);
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(deathAnimTime);
        Destroy(gameObject);
    }
}