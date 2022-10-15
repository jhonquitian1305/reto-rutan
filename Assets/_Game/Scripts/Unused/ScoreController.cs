using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    
    [SerializeField] private int points = 0;
    
    private void Awake()
    {
        Instance = this;
    }
    public void IncreasePoints()
    {
        points++;
        Debug.Log(points);
    }
}
