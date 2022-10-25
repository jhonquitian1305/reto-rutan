using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public PausaCanvas levelLoader;
    public PlayerData playerData;
    public PlayerHealthSystem playerHealthSystem;
    void Start()
    {
        playerHealthSystem = GameObject.FindWithTag("Player").GetComponent<PlayerHealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfBonusLevel();
        CheckIfDead();
    }

    private void CheckIfBonusLevel()
    {
        if (playerData.score >= 10)
        {
            playerData.lives++;
            playerData.score = 0;
        }
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
