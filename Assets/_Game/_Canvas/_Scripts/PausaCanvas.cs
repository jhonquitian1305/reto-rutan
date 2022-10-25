using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PausaCanvas : MonoBehaviour
{
    public PlayerData playerData;
    public int vidasIniciales = 3;
    public Animator animator;
    public AnimationClip animacionFinal;
    public GameObject menuPausa;
    private PlayerInputController playerInputController;

    private void Start()
    {
        playerInputController = GameObject.FindWithTag("Player").GetComponent<PlayerInputController>();
    }

    public void Pausar()
    {
        if (menuPausa == null) return;
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        playerInputController.DisableInput();
    }

    public void Reanudar()
    {
        if (menuPausa == null) return;
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        playerInputController.EnableInput();
    }

    public void Reiniciar(bool isDead)
    {
        StartCoroutine(ReiniciarEscena(isDead));
    }

    public void Volver()
    {
        StartCoroutine(VolverEscena());
    }
    public void UltimaEscena()
    {
        StartCoroutine(VolverEscenaAnterior());
    }

    public void SiguienteEscena()
    {
        StartCoroutine(IrSiguienteEscena());
    }
    IEnumerator IrSiguienteEscena() //Sigue a la siguiente escena cuando el personaje termina un nivel
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerData.SetPlayerData(playerData.lives, playerData.maxHealth, playerData.maxHealth, playerData.score);
    }
    IEnumerator ReiniciarEscena(bool isDead)
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        if (isDead) playerData.currentHealth = 0;
        yield return new WaitForSeconds(animacionFinal.length);
        if(isDead) playerData.currentHealth = playerData.maxHealth;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator VolverEscena()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene(0);
    }
    IEnumerator VolverEscenaAnterior() //Se devuelve a la ultima escena cuando el jugador queda sin vidas
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        playerData.SetPlayerData(vidasIniciales, playerData.maxHealth, playerData.maxHealth, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
