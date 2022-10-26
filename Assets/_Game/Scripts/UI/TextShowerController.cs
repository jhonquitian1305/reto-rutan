using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextShowerController : MonoBehaviour
{
    public Animator textShowerAnimator;
    public TextMeshProUGUI titleTMP, infoTMP;
    public float secs;
    // Start is called before the first frame update
    void Start()
    {
        textShowerAnimator = GetComponent<Animator>();
        ShowPopUp("","");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowPopUp(string newTitle, string newInfo)
    {
        Debug.Log("muestra");
        if (newTitle != "") titleTMP.text = newTitle;
        if (newInfo != "") infoTMP.text = newInfo;
        textShowerAnimator.SetBool("Show", true);
        StartCoroutine(StopPopUp());
    }
    IEnumerator StopPopUp()
    {
        yield return new WaitForSeconds(secs);
        textShowerAnimator.SetBool("Show", false);
    }
}
