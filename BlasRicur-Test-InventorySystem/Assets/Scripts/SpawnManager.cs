using System;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnManager
{
    public static int Wave;

    public static List<GameObject> Enemies = new List<GameObject>();

    public static void EnemyDeath(GameObject gameObject)
    {
        Enemies.Remove(gameObject);
        if(Enemies.Count <= 0)
        {
            Debug.Log("New Wave");
            NextWave();

            //I would use JSon, but I dont have much time to create a perfect system.
            PlayerPrefs.SetInt("Wave", Wave);
            PlayerPrefs.Save();
        }
    }

    public static Action NextWave = () => { Wave++; };
}
