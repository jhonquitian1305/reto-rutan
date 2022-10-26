using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas: MonoBehaviour
{
    public PlayerData playerData;
    public int startingPlayerLives = 3;
    public Animator animator;
    public AnimationClip animacionFinal;
    public void Play()
    {
        StartCoroutine(CambiarEscena());
    }
    public void Exit()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }

    IEnumerator CambiarEscena()
    {
        animator.SetTrigger("Iniciar");
        yield return new WaitForSeconds(animacionFinal.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerData.SetPlayerData(startingPlayerLives, playerData.maxHealth, playerData.maxHealth, 0);
    }
}
