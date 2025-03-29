using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    void Start()
    {
        StartCoroutine(SpawnWave());

        SpawnManager.NextWave += () => { StartCoroutine(SpawnWave()); }; ;
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(5);

        for (int i = 0; i < SpawnManager.Wave * 3; i++)
        {
            yield return new WaitForSeconds(1);
            OnSpawnEnemie(enemy);
        }
    }

    void OnSpawnEnemie(GameObject enemy)
    {
        GameObject instantiated = Instantiate(enemy);
        instantiated.transform.position = transform.position;
        SpawnManager.Enemies.Add(instantiated);
    }
}
