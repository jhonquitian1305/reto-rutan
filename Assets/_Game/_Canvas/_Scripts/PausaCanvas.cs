using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaCanvas : MonoBehaviour
{
    public PlayerData playerData;
    public int playerLives=3, playerMaxHealth=100;
    public Animator animator;
    public AnimationClip animacionFinal;
    public GameObject buttonPausa;
    public GameObject menuPausa;

    public void Pausar()
    {
        Time.timeScale = 0f;
        buttonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
        buttonPausa.SetActive(true);
    }
    public void Reiniciar()
    {
        StartCoroutine(ReiniciarEscena());
    }
    public void Volver()
    {
        StartCoroutine(VolverEscena());
    }
    IEnumerator ReiniciarEscena()
    {
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerData.currentHealth = playerData.maxHealth;
    }
    IEnumerator VolverEscena()
    {
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene(0);
        playerData.currentHealth = playerData.maxHealth;
    }
}
