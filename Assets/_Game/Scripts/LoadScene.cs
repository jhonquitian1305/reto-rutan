using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene : MonoBehaviour
{

    public TextMeshProUGUI levelName;
    public Animator transition;
    public float transitionTime = 4f;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadLevel(nextLevelIndex, "NextLevel"));
        levelName.text = "Capítulo " + nextLevelIndex;
    }

    public void StartNewGame(int lives, int maxHealth, int currentHealth, int score)
    {
        SceneManager.LoadScene(0);
        playerData.SetPlayerData(lives,maxHealth,currentHealth,score);
    }

    IEnumerator LoadLevel(int levelIndex, string animTrigger)
    {
        transition.SetTrigger(animTrigger);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
                    Debug.Log("entra");
            LoadNextLevel();
        }
    }

}
