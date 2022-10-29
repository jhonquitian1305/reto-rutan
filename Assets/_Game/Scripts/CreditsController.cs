using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public AnimationClip _animation;
    public void Start()
    {
        FindObjectOfType<SoundManager>().Play("Background" + SceneManager.GetActiveScene().buildIndex);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<SoundManager>().Stop("Background" + SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(0);  
        }
    }
    public void VolverEscenaAnterior()
    {
        FindObjectOfType<SoundManager>().Stop("Background" + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
}
