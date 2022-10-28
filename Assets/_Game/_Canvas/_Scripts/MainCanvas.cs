using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainCanvas: MonoBehaviour
{
    public PlayerData playerData;
    public int startingPlayerLives = 3;
    public Animator animator;
    public AnimationClip animacionFinal;
    public AudioMixer audioMixer;

    public void Start()
    {
        FindObjectOfType<SoundManager>().Play("Background");
    }

    public void Play()
    {
        FindObjectOfType<SoundManager>().Play("Click");
        StartCoroutine(CambiarEscena());
    }
    public void Exit()
    {
        FindObjectOfType<SoundManager>().Play("Click");
        Debug.Log("Saliendo...");
        Application.Quit();
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }
    IEnumerator CambiarEscena()
    {
        FindObjectOfType<SoundManager>().Stop("Background");
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerData.SetPlayerData(startingPlayerLives, playerData.maxHealth, playerData.maxHealth, 0);
    }
}
