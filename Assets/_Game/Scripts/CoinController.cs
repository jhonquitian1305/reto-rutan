using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.transform.CompareTag("Player"))
        {
            ScoreController.Instance.IncreasePoints();
            Destroy(gameObject);
        }
    }
}
