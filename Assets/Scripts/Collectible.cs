using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static int collectedCount = 0;
    public static int totalCollectibles = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedCount++;
            Destroy(gameObject);

            // Обновить UI
            UI.Instance.UpdateCollectibleCount(collectedCount, totalCollectibles);
        }
    }
}