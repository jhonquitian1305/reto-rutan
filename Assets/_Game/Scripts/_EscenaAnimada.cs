using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _EscenaAnimada : MonoBehaviour
{
    public AnimationClip _animation;
    public void Start()
    {
        FindObjectOfType<SoundManager>().Play("Background" + SceneManager.GetActiveScene().buildIndex);
        Play();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<SoundManager>().Stop("Background" + SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
        }
    }

    public void Play()
    {
        StartCoroutine(CambiarEscena());
    }
    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(_animation.length);
        FindObjectOfType<SoundManager>().Stop("Background" + SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
