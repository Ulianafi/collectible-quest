using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamondPrefab;
    public List<Transform> spawnPoints;
    public int numberOfDiamonds = 5;

    private void Start()
    {
        SpawnDiamonds();
    }

    void SpawnDiamonds()
    {
        ShuffleList(spawnPoints);

        for (int i = 0; i < numberOfDiamonds; i++)
        {

            Transform spawnPoint = spawnPoints[i];

            Instantiate(diamondPrefab, spawnPoint.position, Quaternion.Euler(-90, Random.Range(0, 360), 0));
        }
    }

    void ShuffleList(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Transform temp = list[i];
            int randomIndex = Random.Range(i, list.Count); 
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
