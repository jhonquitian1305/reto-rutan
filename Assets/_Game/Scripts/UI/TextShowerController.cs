using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextShowerController : MonoBehaviour
{
    public Animator textShowerAnimator;
    public TextMeshProUGUI titleTMP, infoTMP;
    public float secs;
    public PausaCanvas pausaCanvas;
    public Button continuar;
    private PlayerInputController playerInputController;
    private bool isActive;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        textShowerAnimator = GetComponent<Animator>();
        playerInputController = GameObject.FindWithTag("Player").GetComponent<PlayerInputController>();
        soundManager = FindObjectOfType<SoundManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPopUp(string newTitle, string newInfo)
    {
        isActive = true;
        continuar.Select();
        pausaCanvas.canPause = false;
        playerInputController.DisableInput();
        if (newTitle != "") titleTMP.text = newTitle;
        if (newInfo != "") infoTMP.text = newInfo;
        textShowerAnimator.SetBool("Show", true);
    }
    public void StopPopUp()
    {
        if (!isActive) return;
        if (soundManager != null) soundManager.Play("Click");
        textShowerAnimator.SetBool("Show", false);
        pausaCanvas.canPause = true;
        isActive = false;
        StartCoroutine(EnableInput());
    }

    IEnumerator EnableInput()
    {
        yield return new WaitForSeconds(0.2f);
        playerInputController.EnableInput();
    }
}
