using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public BoxCollider boxCollider;
    public GameObject portalReactVortex;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        portalReactVortex.SetActive(false);
        sceneHandler = GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPortal()
    {
        boxCollider.enabled = true;
        portalReactVortex.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneHandler.LoadNextLevel();
        }
    }
}
