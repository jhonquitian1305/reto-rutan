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
        public GameObject buttonPausa;
        public GameObject menuPausa;
        private readonly int Iniciar = Animator.StringToHash("Iniciar");
        
        public void Pausar()
        {
            buttonPausa.SetActive(false);
            menuPausa.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Reanudar()
        {
            menuPausa.SetActive(false);
            buttonPausa.SetActive(true);
            Time.timeScale = 1f;
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
            buttonPausa.SetActive(false);
            Time.timeScale = 1f;
            animator.SetTrigger(Iniciar);
            yield return new WaitForSeconds(animacionFinal.length);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerData.currentHealth = playerData.maxHealth;
        }
        
        IEnumerator VolverEscena()
        {
            menuPausa.SetActive(false);
            buttonPausa.SetActive(false);
            Time.timeScale = 1f;
            animator.SetTrigger(Iniciar);
            yield return new WaitForSeconds(animacionFinal.length);
            SceneManager.LoadScene(0);
            playerData.currentHealth = playerData.maxHealth;
        }
    }
}
