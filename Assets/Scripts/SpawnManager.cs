using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<WaveData> waves;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private DayManager dayManager;



    private int CalculateTotalEnemies()
    {
        int totalEnemies = 0 ;
        foreach(WaveData wave in waves)
        {
            
            int enemyCount = wave.enemyCount;
            totalEnemies += enemyCount;
        }

        return totalEnemies;
    }
}
