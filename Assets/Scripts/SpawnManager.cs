using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private List<WaveData> waves;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private DayManager dayManager;

    [SerializeField] private float elapsedTime;
    [SerializeField] private int nextWaveIndex;
    [SerializeField] private bool hasFinishedSpawning;


    private void Start()
    {
        if( dayManager != null)
        {
            dayManager.SetTotalEnemies(CalculateTotalEnemies());
        }
    }
    private int CalculateTotalEnemies()
    {
        if (waves == null) return  0;
        int totalEnemies = 0 ;
        foreach(WaveData wave in waves)
        {
            if( wave == null) continue;
            totalEnemies += wave.enemyCount;
        }

        return totalEnemies;
    }
}
