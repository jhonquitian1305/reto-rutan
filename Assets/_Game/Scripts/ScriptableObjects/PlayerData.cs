using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data", fileName = "New Player Data")]
public class PlayerData : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
    public float score;
}
