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

        WaitForSeconds wait = new WaitForSeconds(1 + 1 / SpawnManager.Wave);
        for (int i = 0; i < SpawnManager.Wave * 3; i++)
        {
            yield return wait;
            OnSpawnEnemie(enemy);
        }
    }

    void OnSpawnEnemie(GameObject enemy)
    {
        GameObject instantiated = Instantiate(enemy);
        instantiated.transform.position = transform.position;
        instantiated.GetComponent<ModelCharacter>().maxSpeed = 0.1f + SpawnManager.Wave * .1f;
        SpawnManager.Enemies.Add(instantiated);
    }
}
