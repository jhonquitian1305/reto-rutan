using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvas: MonoBehaviour
{
    public PlayerData playerData;
    public int playerLives=3, playerMaxHealth=100;
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerData.SetPlayerData(playerLives, playerMaxHealth, playerMaxHealth, 0);
    }

    public void Exit()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
