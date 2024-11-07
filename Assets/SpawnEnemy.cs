using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs; // Array to hold the enemy prefabs

    [SerializeField]
    private GameObject[] bossPrefabs;
    
    [SerializeField]
    private float spawnInterval = 5.0f; // Time interval between spawns

    [SerializeField]
    private BoxCollider[] spawnArea; // BoxCollider to define the spawn area

    [SerializeField]
    private bool bossSpawned = false;

    private int spawnLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn enemies at regular intervals
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if(VerAptosController.instance != null)
            {
                if(VerAptosController.instance.wave % 5 == 0)
                {
                    spawnInterval = 1f;
                    if(!bossSpawned)
                    {
                        bossSpawned=true;
                        // Get a random position within the BoxCollider
                        int randomBossSpawn = Random.Range(0, spawnArea.Length);

                        Vector3 spawnBossPosition = GetRandomPositionInBoxCollider(spawnArea[randomBossSpawn]);

                        // Instantiate the enemy at the random position
                        switch(VerAptosController.instance.wave)
                        {
                            case 5:
                                Instantiate(bossPrefabs[0], spawnBossPosition, Quaternion.identity);
                                break;
                            case 10:
                                Instantiate(bossPrefabs[1], spawnBossPosition, Quaternion.identity);
                                break;
                            case 15:
                                Instantiate(bossPrefabs[2], spawnBossPosition, Quaternion.identity);
                                break;
                            default:
                                Instantiate(bossPrefabs[0], spawnBossPosition, Quaternion.identity);
                                break;
                        }
                    }
                }
                else
                {
                    spawnInterval = 5.0f - VerAptosController.instance.wave*0.6f;
                    bossSpawned = false;
                }
            }
            yield return new WaitForSeconds(spawnInterval);
            int spawn = spawnLimit;
            if (VerAptosController.instance.wave < 10)
            {
                spawn = VerAptosController.instance.wave;
            }
            for (int i =0;i<spawn;i++)
            {
                // Randomly select an enemy prefab
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyToSpawn = enemyPrefabs[randomIndex];

                // Get a random position within the BoxCollider
                int randomSpawn = Random.Range(0, spawnArea.Length);

                Vector3 spawnPosition = GetRandomPositionInBoxCollider(spawnArea[randomSpawn]);

                // Instantiate the enemy at the random position
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }

    // Method to get a random position within a BoxCollider
    private Vector3 GetRandomPositionInBoxCollider(BoxCollider boxCollider)
    {
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        // Convert local position to world position
        return boxCollider.transform.TransformPoint(randomPosition);
    }
}

