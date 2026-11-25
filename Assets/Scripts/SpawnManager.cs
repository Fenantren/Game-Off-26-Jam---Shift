
using System.Collections.Generic;

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
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        elapsedTime = 0;
        nextWaveIndex = 0;
    }

    private void Update()
    {
        if (hasFinishedSpawning == false)
        {
            elapsedTime += Time.deltaTime;
            
            SpawnDueWaves();
        }
    }
    private int CalculateTotalEnemies()
    {
        if (waves == null) return  0;
        int totalEnemies = 0 ;
        foreach(WaveData wave in waves)
        {
            if( wave == null) continue;
            if (wave.enemyCount < 0) continue;
            totalEnemies += wave.enemyCount;
        }

        return totalEnemies;
    }

    private void SpawnDueWaves()
    {
        if (waves == null)
        {
            FinishSpawning();
            return;
        }
        
        
        while (nextWaveIndex < waves.Count)
        {
            WaveData wave = waves[nextWaveIndex];

            if (wave == null) 
            {
                nextWaveIndex++;
                continue;
            }
            
            if (elapsedTime < wave.spawnTime) break;

            if (!CanSpawnWave()) return;
            
            SpawnWave(wave);
            nextWaveIndex++;
        }
        
        if ( nextWaveIndex >= waves.Count )
        {

            FinishSpawning();
        }
    }
    
    //Prevents a wave from being marked as processed if its references are invalid
    private bool CanSpawnWave()
    {
        //checks pre spawning (prefab, array validity / array empty ) 
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0) 
            return false;
        //Checks for invalid position
        foreach( Transform point in spawnPoints)
        {
            if (point == null) 
                return false;
        }

        return true;

    }

    private void SpawnWave(WaveData wave)
    {
        if (wave == null || wave.enemyCount <= 0)
            return;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            int spawnIndex = i % spawnPoints.Length;
            
            GameObject enemy = Instantiate( enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation );
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            
            if(enemyHealth != null)
            {
                enemyHealth.Initialize(dayManager);
            }
            else
            {
                Debug.LogError("Enemy prefab is missing health script!!! ");
            }
        }
    }
    private void FinishSpawning()
    {
        if (hasFinishedSpawning) return;

        hasFinishedSpawning = true;

        if(dayManager != null)
        {
            dayManager.SetSpawningFinished();
        }
    }
}
