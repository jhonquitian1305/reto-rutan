using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public PlayerData playerData;
    
    [SerializeField] private AudioSource collectCoinEffect;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.CompareTag("Player"))
        {
            playerData.score++;
            Destroy(gameObject);
            collectCoinEffect.Play();
        }
    }
}
