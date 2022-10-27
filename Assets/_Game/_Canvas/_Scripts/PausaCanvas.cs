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
    public GameObject menuPanel;
    public bool canPause;
    private PlayerInputController playerInputController;
    public string audioName;

    private void Start()
    {
        FindObjectOfType<SoundManager>().Play(audioName + SceneManager.GetActiveScene().buildIndex);
        canPause = true;

        playerInputController = GameObject.FindWithTag("Player").GetComponent<PlayerInputController>();
        animator = GameObject.FindWithTag("Transition").GetComponent<Animator>();
    }

    public void Pausar()
    {
        if (menuPanel == null || !canPause) return;
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
        playerInputController.DisableInput();
    }

    public void Reanudar()
    {
        if (menuPanel == null || !canPause) return;
        menuPanel.SetActive(false);
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
        playerData.SetPlayerData(playerData.lives, playerData.maxHealth, playerData.maxHealth, playerData.score);
        FindObjectOfType<SoundManager>().Stop(audioName + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator ReiniciarEscena(bool isDead)
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        if(isDead) playerData.currentHealth = playerData.maxHealth;
        playerData.score = 0; 
        FindObjectOfType<SoundManager>().Stop(audioName + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator VolverEscena()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        FindObjectOfType<SoundManager>().Stop(audioName + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
    IEnumerator VolverEscenaAnterior() //Se devuelve a la ultima escena cuando el jugador queda sin vidas
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        playerData.SetPlayerData(vidasIniciales, playerData.maxHealth, playerData.maxHealth, 0);
        FindObjectOfType<SoundManager>().Stop(audioName + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
