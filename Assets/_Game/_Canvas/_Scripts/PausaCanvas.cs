using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

namespace _Game._Canvas._Scripts
{
    public class PausaCanvas : MonoBehaviour
    {
        public PlayerData playerData;
        public int playerLives=3, playerMaxHealth=100;
        public Animator animator;
        public AnimationClip animacionFinal;
        public GameObject menuPausa;
        private readonly int Iniciar = Animator.StringToHash("Iniciar");
        private PlayerInputController playerInputController;

        private void Start()
        {
            playerInputController = GameObject.FindWithTag("Player").GetComponent<PlayerInputController>();
        }

        public void Pausar()
        {
            menuPausa.SetActive(true);
            Time.timeScale = 0f;
            playerInputController.DisableInput();
        }

        public void Reanudar()
        {
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
            playerInputController.EnableInput();
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
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
            animator.SetTrigger(Iniciar);
            yield return new WaitForSeconds(animacionFinal.length);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerData.currentHealth = playerData.maxHealth;
        }
        IEnumerator VolverEscena()
        {
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
            animator.SetTrigger(Iniciar);
            yield return new WaitForSeconds(animacionFinal.length);
            SceneManager.LoadScene(0);
            playerData.currentHealth = playerData.maxHealth;
        }
    }
}
