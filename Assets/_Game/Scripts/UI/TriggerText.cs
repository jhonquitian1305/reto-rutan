using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    private TextShowerController textShowerController;
    private BoxCollider boxCollider;
    public string text, title;
    public float secs;
    // Start is called before the first frame update
    void Start()
    {
        textShowerController = GameObject.FindWithTag("TextShower").GetComponent<TextShowerController>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || !other.isTrigger)
        {
            textShowerController.secs = secs;
            textShowerController.ShowPopUp(title, text);
            boxCollider.enabled = false;
        }
    }
}
