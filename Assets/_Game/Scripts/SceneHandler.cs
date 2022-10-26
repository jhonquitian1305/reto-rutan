using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public PausaCanvas levelLoader;
    public PlayerData playerData;
    public PlayerHealthSystem playerHealthSystem;
    public List<GameObject> essentialEnemies;
    public PortalController portalController;
    void Start()
    {
        playerHealthSystem = GameObject.FindWithTag("Player").GetComponent<PlayerHealthSystem>();
        foreach(GameObject enemy in essentialEnemies)
        {
            enemy.GetComponent<EnemyHealthSystem>().IsEssential = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfBonusLevel();
        CheckIfDead();
        CheckIfSceneCompleted();
    }

    private void CheckIfBonusLevel()
    {
        if (playerData.score >= 10)
        {
            playerData.lives++;
            playerData.score = 0;
        }
    }

    private void CheckIfSceneCompleted()
    {
        if (essentialEnemies.Count <= 0) portalController.OpenPortal();
    }
    public void LoadNextLevel()
    {
        levelLoader.SiguienteEscena();
    }
    private void CheckIfDead() 
    {
        if (playerHealthSystem.isDead)
        {
            if (playerData.lives > 0)
            {
                StartCoroutine(ReiniciarEscena(true));

            }
            else
            {
                StartCoroutine(IrUltimaEscena());
            }
        }
    }

    IEnumerator ReiniciarEscena(bool isDead)
    {
        yield return new WaitForSeconds(3);
        levelLoader.Reiniciar(isDead);
    }
    IEnumerator IrUltimaEscena()
    {
        yield return new WaitForSeconds(3);
        levelLoader.UltimaEscena();
    }
    
}
