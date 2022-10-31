using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguajeSelected : MonoBehaviour
{
    public bool spanishSelect;
    public bool englishSelect;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        spanishSelect = true;
        englishSelect = false;
    }
}
