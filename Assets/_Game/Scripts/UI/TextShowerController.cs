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
    // Start is called before the first frame update
    void Start()
    {
        textShowerAnimator = GetComponent<Animator>();
        playerInputController = GameObject.FindWithTag("Player").GetComponent<PlayerInputController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPopUp(string newTitle, string newInfo)
    {
        continuar.Select();
        pausaCanvas.canPause = false;
        playerInputController.DisableInput();
        if (newTitle != "") titleTMP.text = newTitle;
        if (newInfo != "") infoTMP.text = newInfo;
        textShowerAnimator.SetBool("Show", true);
    }
    public void StopPopUp()
    {
        playerInputController.EnableInput();
        textShowerAnimator.SetBool("Show", false);
        pausaCanvas.canPause = true;
    }
}
