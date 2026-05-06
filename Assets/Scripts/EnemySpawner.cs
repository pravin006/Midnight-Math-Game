// // using UnityEngine;

// // public class EnemySpawner : MonoBehaviour
// // {
// //     public GameObject enemyPrefab;
// //     public float distanceBetweenCheck;
// //     public float heightOfCheck = 100f, rangeOfCheck = 200f;
// //     public LayerMask layerMask;

// //     [System.Serializable]
// //     public struct SpawnArea
// //     {
// //         public Vector2 min;
// //         public Vector2 max;
// //     }
// //     public SpawnArea[] areas;
 
 
// //     public void SpawnEnemies(float spawnChance)
// //     {
// //         foreach (SpawnArea area in areas)
// //         {
// //             for (float x = area.min.x; x < area.max.x; x += distanceBetweenCheck)
// //             {
// //                 for (float z = area.min.y; z < area.max.y; z += distanceBetweenCheck)
// //                 {
// //                     RaycastHit hit;

// //                     if (Physics.Raycast(new Vector3(x, heightOfCheck, z),
// //                         Vector3.down, out hit, rangeOfCheck, layerMask))
// //                     {
// //                         if (spawnChance > Random.Range(0f, 101f))
// //                         {
// //                             Instantiate(enemyPrefab,
// //                                 hit.point,
// //                                 Quaternion.Euler(0, Random.Range(0, 360), 0),
// //                                 transform);
// //                         }
// //                     }
// //                 }
// //             }
// //         }
// //     }
// // }

using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float distanceBetweenCheck = 10f;
    [SerializeField] private float sampleRadius = 3f;
    [SerializeField] private AudioManager audioManager;

    [System.Serializable]
    public struct SpawnArea
    {
        public Vector2 min;
        public Vector2 max;
    }

    [SerializeField] private SpawnArea[] areas;

    public void SpawnEnemies(float spawnChance)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner: enemyPrefab is missing.");
            return;
        }

        if (audioManager != null)
            audioManager.PlaySpawnEnemies();

        foreach (SpawnArea area in areas)
        {
            for (float x = area.min.x; x < area.max.x; x += distanceBetweenCheck)
            {
                for (float z = area.min.y; z < area.max.y; z += distanceBetweenCheck)
                {
                    if (Random.Range(0f, 100f) > spawnChance)
                        continue;

                    Vector3 checkPos = new Vector3(x, 0f, z);

                    if (NavMesh.SamplePosition(checkPos, out NavMeshHit hit, sampleRadius, NavMesh.AllAreas))
                    {
                        GameObject newEnemy = Instantiate(
                            enemyPrefab,
                            hit.position,
                            Quaternion.Euler(0f, Random.Range(0f, 360f), 0f)
                        );

                        NavMeshAgent agent = newEnemy.GetComponent<NavMeshAgent>();
                        if (agent != null)
                        {
                            agent.speed = 1f;
                            agent.Warp(hit.position);
                        }
                    }
                }
            }
        }
    }
}