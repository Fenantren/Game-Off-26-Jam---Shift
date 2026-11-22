using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnTransform;

    [SerializeField] int spawnCount = 3;

    private void Start()
    {
        // Safety check to prevent DivideByZeroException if the array is empty
        if (spawnTransform == null || spawnTransform.Length == 0) return;

        for (int i = 0;  i < spawnCount; i++)
        {
            int spawnIndex = i % spawnTransform.Length;
            Instantiate(enemyPrefab, spawnTransform[spawnIndex].position, spawnTransform[spawnIndex].rotation);
        }
    }
}
