using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data", fileName = "New Player Data")]
public class PlayerData : ScriptableObject
{
    public int lives = 3;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float score = 0;
}
