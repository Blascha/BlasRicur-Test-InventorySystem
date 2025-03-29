using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    IEnumerator Start()
    {
        for(int i = 0; i < 10; i++)
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
