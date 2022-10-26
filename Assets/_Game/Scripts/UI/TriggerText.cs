using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    private TextShowerController textShowerController;
    private SphereCollider sphereCollider;
    public string text, title;
    public float secs;
    // Start is called before the first frame update
    void Start()
    {
        textShowerController = GameObject.FindWithTag("TextShower").GetComponent<TextShowerController>();
        sphereCollider = GetComponent<SphereCollider>();
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
            textShowerController.ShowPopUp(text, title);
            sphereCollider.enabled = false;
        }
    }
}
