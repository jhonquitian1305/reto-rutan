using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public PlayerData playerData;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.CompareTag("Player"))
        {
            playerData.score++;
            collider.gameObject.GetComponent<PlayerHealthSystem>().UpdateHealth(10,false);
            Destroy(gameObject);
        }
    }
}
