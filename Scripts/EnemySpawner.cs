using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemySpawnPoints;
    public GameObject[] enemyPrefabs;

    private int index;

    private void Start()
    {
        foreach (GameObject point in enemySpawnPoints)
        {
            System.Random random = new System.Random();
            index = random.Next(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[index], point.transform.position, Quaternion.identity, gameObject.transform);
            Destroy(point);
        }   
    }
}
